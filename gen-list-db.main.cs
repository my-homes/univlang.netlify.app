//+#nuget Global.Sys
using Global;
using System;
using System.IO;
using System.Text;
using static Global.EasyObject;
using static Global.Sys;

Global.Sys.SetupConsoleUTF8();
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Encoding shiftJisEncoding = Encoding.GetEncoding("Shift_JIS");
try
{
    SilentFlag = true;
    string dbFolder = HomeFolder("youtube-db");
    var propsFile = new FileInfo(HomeFile("youtube-db", "video-list.litedb"));
    var props = new LiteDBProps(propsFile);
    string output = GetProcessStdout(
        Encoding.UTF8,
        "my-ls.exe",
        "/p/@youtube-m4a"
    );
    var lines = TextToLines(output);
    var dict = NewObject();
    var array = NewArray();
    foreach( var line in lines )
    {
        var info = MediaInfo.ParseMediaUrl(line);
        if ( info != null )
        {
            string id = info["videoId"].Cast<string>();
            if (id == null) continue;
            array.Add(id);
            dict[id] = info;
        }
    }
    props.Put("m4a-list", array);
}
catch (Exception e)
{
    Crash(e);
}
