using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MediaConverter2
{
    public partial class Form1 : Form
    {
        XDocument xdoc;
        XMLForm form2;
        ConvertProperties cp;
        Command ccmd;
        List<Process> procList;

        public Form1()
        {
            InitializeComponent();

            //ファイル名を保存するXMLファイルを生成
            xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XComment("ItemInfo"),
                new XElement("Items")
            );

            //XMLファイルが更新されたときのイベントハンドラ
            xdoc.Changed += new EventHandler<XObjectChangeEventArgs>(
                (sender, cea) =>
                {
                    //画面の更新をかける
                    updateScreenData();
                    form2.SetLableText(xdoc.ToString());
                }
            );

            //デバッグ用フォームを生成
            form2 = new XMLForm();
            form2.Show();

            //実行時のプロパティを設定
            cp = new ConvertProperties();

            //コマンド生成用クラスを設定
            ccmd = new Command();

            //プロセス管理用リスト
            procList = new List<Process>();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // XMLを保存する
            xdoc.Save(@"Files.xml");
        }

        private void processingListBox_DragDrop(object sender, DragEventArgs e)
        {
            //ファイルパスを読み込む
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i];
                //Text += fileName + "\r\n";

                // ファイルをリストに追加する
                var fs = xdoc.Element("Items");
                fs.Add(new XElement("Item",
                    new XElement("Time", String.Format("{0:yyyyMMdd_HHmmssfff_}", DateTime.Now)
                                    + i.ToString("X6")),
                    new XElement("Path", fileName),
                    new XElement("Status", "Suspend"),
                    new XElement("ProcessType", "0"),
                    new XElement("Name", System.IO.Path.GetFileName(fileName)),
                    new XElement("Deleted", "false"),
                    new XElement("ProcessID", "0")
                    ));
                Debug.Print(fileName);
            }
        }

        private void processingListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //リストの表示を更新
        private void updateScreenData()
        {
            //リストをクリア
            //processingListBox.Items.Clear();
            
            // 処理中/待ちリストを更新
            IEnumerable<Object> items = xdoc.Descendants("Item")
                                        .Where(item => item.Element("Deleted").Value == "false"
                                                        && item.Element("Status").Value != "Finished")
                                        .OrderByDescending(item => (string)item.Element("Time"))
                                        .Select(item => new { Display = string.Format(@"{0}({1})",
                                                                        item.Element("Name").Value,
                                                                        item.Element("Status").Value),
                                                              Value = item.Element("Time").Value }
                                        ).ToList();

            processingListBox.DataSource = items;
            processingListBox.DisplayMember = "Display";
            processingListBox.ValueMember = "Value";

            // 処理済みリストを更新
            items = xdoc.Descendants("Item")
                    .Where(item => item.Element("Deleted").Value == "false"
                                    && item.Element("Status").Value == "Finished")
                    .OrderByDescending(item => (string)item.Element("Time"))
                    .Select(item => new {
                        Display = string.Format(@"{0}({1})",
                                                    item.Element("Name").Value,
                                                    item.Element("Status").Value),
                        Value = item.Element("Time").Value
                    }
                    ).ToList();

            finishedListBox.DataSource = items;
            finishedListBox.DisplayMember = "Display";
            finishedListBox.ValueMember = "Value";

        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            //コンテキストメニューが開いたらリストをアクティブにする
            processingListBox.Select();
        }

        //ファイルの削除ボタンが押された
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //選択されている要素を抽出
            foreach(dynamic item in processingListBox.SelectedItems)
            {
                String s = (item.Value);

                var p = xdoc.Descendants("Item").Where(i => i.Element("Time").Value == s);
                p.First().Element("Deleted").SetValue("true");
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //全選択する
            SendKeys.SendWait("{HOME}+{END}");
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            OptionWindow opw = new OptionWindow();
            opw.Show();
        }

        //ファイルに処理をかける
        private void ExeFiles()
        {
            //
            int num = xdoc.Descendants("Item")
                        .Count(n => n.Element("Status").Value == "Processing");

            Debug.Print("Number of Processing:" + num.ToString());

            //ファイルを処理に書ける
            if(num < cp.ParallelNum)
            {
                IEnumerable<XElement> xe;
                xe =  xdoc.Descendants("Item")
                      .Where(item => item.Element("Deleted").Value == "false"
                                      && item.Element("Status").Value == "Suspend")
                      .OrderByDescending(item => (string)item.Element("Time"))
                      .Take(cp.ParallelNum - num);

                foreach(XElement p in xe)
                {
                    //ファイルに対してコマンドを実行する
                    String c = ccmd.GetExecCommandString(p.Element("Path").Value);

                    Process proc = new Process();
                    proc.StartInfo.FileName = "timeout";
                    proc.StartInfo.Arguments = @"/T 2";
                    proc.SynchronizingObject = this;
                    proc.EnableRaisingEvents = true;
                    proc.Exited += new EventHandler(ProcExisted);
                    proc.Start();

                    //実行ステータスを変える
                    p.Element("Status").SetValue("Processing");

                    //プロセスIDを追加する
                    p.Element("ProcessID").SetValue(proc.Id);

                    //リストにプッシュ
                    procList.Add(proc);
                }
            }
        }

        //ファイルの処理が終了した
        private void ProcExisted(object sender, EventArgs e)
        {
            //どれが終了したのか検索
            for (int i = 0; i < procList.Count(); i++)
            {
                Process p = procList[i];

                if (p.HasExited)
                {
                    //終了しているプロセス
                    //プロセスIDを取得してファイルデータベースと紐づけ
                    IEnumerable<XElement> xe;
                    xe = xdoc.Descendants("Item")
                          .Where(item => item.Element("Deleted").Value == "false"
                                          && item.Element("Status").Value == "Processing"
                                          && item.Element("ProcessID").Value == p.Id.ToString());
                    if(xe.Count() > 0)
                    {
                        xe.First().Element("Status").SetValue("Finished");
                    }
                    
                    procList.Remove(p);

                    i = 0;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ExeFiles();
        }
    }
}
