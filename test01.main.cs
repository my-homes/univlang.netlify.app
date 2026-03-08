// Project Name: test01.main.cs
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
    ShowDetail = true;
    Log("ハロー©");
    Log(new { args });
    RunCommand(
        "git.exe",
        "rev-parse",
        "--show-toplevel"
    );
    SilentFlag = true;
    string output = GetProcessStdout(
        Encoding.UTF8,
        "git.exe",
        "rev-parse",
        "--show-toplevel"
    );
    Log(output, "output");
    string tmpFile = HomeFile("tmp", "output.txt");
    File.WriteAllText(tmpFile, output);
    string output2 = File.ReadAllText(tmpFile);
    Log(output2, "output2");
    string ls = GetProcessStdout(
        Encoding.UTF8,
        "my-ls.exe",
        //HomeFolder()
        @"P:\@porn++++"
    );
    var lines = TextToLines(ls);
    //Log(lines, "lines");
    foreach(var line in lines)
    {
        //Log(line, "line");
        var info = MediaInfo.ParseMediaUrl(line);
        if (info != null)
        {
            DumpObjectAsJson(info, compact: true, keyAsSymbol: true);
        }
    }
}
catch (Exception e)
{
    Crash(e);
}
