using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaConverter2
{
    class ConvertProperties
    {
        public Int32 ParallelNum;      //処理の同時実行数

        public String SettingFilePath; //設定ファイルのパス

        public ConvertProperties()
        {
            ParallelNum = 2;
        }
    }
}
