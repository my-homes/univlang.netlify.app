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
    SetCwd(HomeFolder("@sub", "nuget.org", "univlang"));
    string fileName = AdjustFileName("tmp.https://www.youtube.com/watch?v=pTxCQjZooQ8&list=PLTvSv0jkjbk_EhZwZjDeNJIIGK25yNGt8");
    Log(fileName);
    File.WriteAllText(fileName, "ハロー©");
    string homeFile = HomeFile("@sub", "nuget.org", "univlang", "tmp.https://www.youtube.com/watch?v=pTxCQjZooQ8&list=PLTvSv0jkjbk_EhZwZjDeNJIIGK25yNGt8");
    Log(homeFile);
    homeFile = homeFile.Replace("/", @"\");
    Log(homeFile);
    File.WriteAllText(homeFile, "ハロー©2");
}
catch (Exception e)
{
    Crash(e);
}
