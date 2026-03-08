//+#nuget Global.Sys
using System;
using System.IO;
using System.Text.RegularExpressions;
using static Global.EasyObject;
using static Global.Sys;
try
{
    SetupConsoleUTF8();
    string allText = "";
    for (int i = 0; i < args.Length; i++)
    {
        string arg = args[i];
        arg = Global.Sys.CygpathWindows(arg);
        string text = File.ReadAllText(arg);
        text = text.Trim() + "\n";
        text = Regex.Replace(text, @"<p>\s*</p>", "", RegexOptions.Multiline);
        text = text.Replace("</head><body>", "");
        allText += text;
    }
    var lines = TextToLines(allText);
    foreach( var line in lines )
    {
        //Log(line);
        //if (!line.StartsWith("<p class=")) { continue; }
        if (!line.StartsWith("<p")) { continue; }
        Console.Write(line + "\n");
    }
}
catch (Exception e)
{
    Crash(e);
}
