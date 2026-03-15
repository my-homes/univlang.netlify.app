//+#nuget Global.Sys
//+#inc my-common.cs
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
    //SilentFlag = true;
    var props = Local.MyCommon.My_Youtube_Props("m4a-list.litedb");
    props.DeleteAll();
    //var lines = Local.MyCommon.My_LS("/p/@youtube-m4a");
    var lines = Local.MyCommon.My_LS_Latest("/p/@youtube-m4a", 100);
    foreach ( var line in lines )
    {
        Log(line);
        var info = MediaInfo.ParseMediaUrl(line);
        if ( info != null )
        {
            string id = info["videoId"].Cast<string>();
            if (id == null) continue;
            props.Put(id, info);
        }
    }
}
catch (Exception e)
{
    Crash(e);
}
