//+#nuget Global.Sys
//+#inc my-common.cs
using Global;
using System;
using System.IO;
using System.Text;
using static Global.EasyObject;
using static Global.Sys;

SetupConsoleUTF8();
try
{
    ShowDetail = false;
    Log("ハロー©");
    Log(new { args });
    var lines = Local.MyCommon.My_LS("/p/####");
    Log(lines);
    foreach (var line in lines)
    {
        Log(line);
        var ifno = MediaInfo.ParseMediaUrl(line);
        Log(ifno);
    }
}
catch (Exception e)
{
    Crash(e);
}
