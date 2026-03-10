//+#nuget Global.Sys
using Global;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    var propsFile = new FileInfo(HomeFile("youtube-db", "m4a-list.litedb"));
    var props = new LiteDBProps(propsFile);
    //var videoListFile = new FileInfo(HomeFile("youtube-db", "video-list.litedb"));
    //var videoListProps = new LiteDBProps(videoListFile);
    //var videoList = videoListProps.Get("m4a-list");
    //var propsFile = new FileInfo(HomeFile("youtube-db", "props1.litedb"));
    //var props = new LiteDBProps(propsFile);
    //var history = props.Get("history", NewArray());
    var videoList = NewArray();
    var keys = props.Keys;
    foreach (var key in keys)
    {
        videoList.Add(props.Get(key));
    }
    //Log(videoList);
    //Environment.Exit(0);
    var array = videoList;
    array = array.Shuffle().Take(5);
    Log(array);
    //Exit(0);
    //var list = array.AsStringList;
    string? title = null;
    string m3u = "";
    string txt = "";
    string mediaMonkey = "#EXTM3U\n#EXTENC: UTF-8\n";
    var mdockArray = NewArray();
    //var dict = NewObject();
    foreach (var info in array.AsList!)
    {
        mdockArray.Add(info);
        string id = info["id"].Cast<string>();
        string fileName = info["name"].Cast<string>();
        if (title == null)
        {
            title = Sys.AdjustFileName(fileName);
            mediaMonkey += $"#PLAYLIST:{fileName}\n";
        }
        m3u += fileName;
        m3u += "\n";
        txt += $"https://youtu.be/{id}";
        txt += "\n";
        mediaMonkey += $"#EXTINF:-1,{fileName}\n";
        mediaMonkey += $"https://www.youtube.com/watch?v={id}\n";
        //history.Add(new { id, fileName });
        //props.Put("history", history);
    }
    Log(m3u, "m3u");
    var dt = FromObject(DateTime.Now);
    var today = Sys.DateStringCompact(DateTime.Now);
    string dtString = dt.Cast<string>();
    dtString = Global.Sys.AdjustFileName(dtString);
    Log(dtString);
    string m3uFileName = $"!! {today}@{title}.m3u";
    Log(m3uFileName);
    Sys.SetCwd(Sys.CygpathWindows("/p/@youtube-m4a"));
    File.WriteAllText(m3uFileName, m3u);
    var list = array.AsList!.Select(x => x["id"].Cast<string>()).ToList();
    string url = $"https://www.youtube.com/watch_videos?video_ids={String.Join(",", list)}";
    Log(url, "url");

    //OpenUrl(url);
    Log(title, "title");
    string txtFileName = $"!! {today}@{title}.txt";
    Log(txtFileName);
    SetCwd(@"G:\マイドライブ\@video");
    File.WriteAllText(txtFileName, txt);
    string mmFileName = $"!! {today}@{title}.m3u";
    SetCwd(HomeFolder("@playlist"));
    File.WriteAllText(mmFileName, mediaMonkey);
    SetCwd(Sys.HomeFolder("@md"));
    if (!File.Exists("@@tmp.md"))
    {
        File.WriteAllText("@@tmp.md", $"#! /usr/bin/env open-markdown\n# !!!!{Sys.DateTimeString(DateTime.Now)}\n\n");
    }
    using (StreamWriter sw = File.AppendText("@@tmp.md"))
    {
        var idList = mdockArray.AsList!.Select(x => x["videoId"].Cast<string>()).ToList();
        string firstId = idList[0];
        string url2 = $"https://www.youtube.com/watch_videos?video_ids={String.Join(",", idList)}";
        sw.Write($"## ★[{title} 等](https://www.youtube.com/watch_videos?video_ids={String.Join(",", idList)})\n");
        var eo = findYideoInfo(firstId);
        if (eo != null)
        {
            int lastIndex = eo["detail"]["Thumbnails"].Count - 1;
            string thumUrl = eo["detail"]["Thumbnails"][lastIndex]["Url"].Cast<string>();
            Log(thumUrl, "thumUrl");
            sw.Write($"[![*]({thumUrl})](https://www.youtube.com/watch_videos?video_ids={String.Join(",", idList)})\n");
        }
    }
    //System.Diagnostics.Process.Start(Sys.HomeFolder("@md"));
    string netlifyDir = HomeFolder("@sub", "nuget.org", "univlang");
    SetCwd (netlifyDir);
    if (!File.Exists("@@playlist.md"))
    {
        File.WriteAllText("@@playlist.md", "#! /usr/bin/env open-markdown\n");
    }
    using (StreamWriter sw = File.AppendText("@@playlist.md"))
    {
        var idList = mdockArray.AsList!.Select(x => x["videoId"].Cast<string>()).ToList();
        string firstId = idList[0];
        string url2 = $"https://www.youtube.com/watch_videos?video_ids={String.Join(",", idList)}";
        var eo = findYideoInfo(firstId);
        if (eo != null)
        {
            int lastIndex = eo["detail"]["Thumbnails"].Count - 1;
            string thumUrl = eo["detail"]["Thumbnails"][lastIndex]["Url"].Cast<string>();
            Log(thumUrl, "thumUrl");
            sw.Write($"<p class='example'><a target='_blank' href='{url2}'>★{title} 等</a><br /><a target='_blank' href='{url2}'><img src='{thumUrl}' /></a><p>\n");
        }
    }
    //DumpObjectAsJson(history, keyAsSymbol: true);
    //EasyObject rev = history.Reverse().Take(5);
    //Log(rev, "rev");
}
catch (Exception e)
{
    Crash(e);
}
Process OpenUrl(string url)
{
    ProcessStartInfo pi = new ProcessStartInfo()
    {
        FileName = url,
        UseShellExecute = true,
    };
    return Process.Start(pi);
}
EasyObject? findYideoInfo(string id)
{
    var fol = HomeFolder("youtube-db");
    var cwd = GetCwd();
    SetCwd(fol);
    var eo = FromFile($"{id}.json", ignoreErrors: false);
    SetCwd(cwd);
    return eo;
}
