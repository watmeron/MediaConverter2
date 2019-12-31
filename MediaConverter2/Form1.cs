using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public Form1()
        {
            InitializeComponent();

            //ファイル名を保存するXMLファイルを生成
            xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XComment("ItemInfo"),
                new XElement("Items")
            );
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
                processingListBox.Items.Add(fileName);
            }
        }
    }
}
