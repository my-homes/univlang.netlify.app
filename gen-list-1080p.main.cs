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
    var propsFile = new FileInfo(HomeFile("youtube-db", "1080p-list.litedb"));
    var props = new LiteDBProps(propsFile);
    props.DeleteAll();
    string output = GetProcessStdout(
        Encoding.UTF8,
        "my-ls.exe",
        "/p/@youtube-1080p"
    );
    var lines = TextToLines(output);
    foreach( var line in lines )
    {
        Log(line);
        var info = MediaInfo.ParseMediaUrl(line);
        if ( info != null )
        {
            string id = info["videoId"].Cast<string>();
            if (id == null) continue;
            info["site"] = "youtube";
            props.Put(id, info);
        }
    }
}
catch (Exception e)
{
    Crash(e);
}
