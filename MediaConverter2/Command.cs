using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ファイルに対して実行するコマンドを管理する
 * 
 */

namespace MediaConverter2
{
    class Command
    {
        //ファイルパスに応じて実行するコマンドを返す
        public String GetExecCommandString(String FilePath)
        {
            return "timeout /T 1";
        }

        //コマンドオプションを設定する
        public void SetCommandOptions()
        {
            return;
        }
    }
}
