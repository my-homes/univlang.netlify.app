//+#nuget Global.Sys
using Global;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Global.Sys;

namespace Local
{
    public static class MyCommon
    {
        public static List<string> My_LS(string dir)
        {
            string ls = GetProcessStdout(
                Encoding.UTF8,
                "my-ls.exe",
                dir
            );
            return TextToLines(ls);
        }
        public static LiteDBProps My_Youtube_Props(string dbName)
        {
            var propsFile = new FileInfo(HomeFile("youtube-db", dbName));
            return new LiteDBProps(propsFile);
        }
    }
}
