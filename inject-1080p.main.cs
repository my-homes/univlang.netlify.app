//+#nuget Global.Sys@2026.311.813.14
//+#inc my-common.cs
using Global;
using System;
using static Global.EasyObject;
using static Global.Sys;

SetupConsoleUTF8();
try
{
    ShowDetail = false;
    SilentFlag = true;
    Log("ハロー©");
    Log(new { args });
    var props = Local.MyCommon.My_Youtube_Props("video-list.litedb");
    var lines = Local.MyCommon.My_LS("/p/@youtube-1080p");
    foreach (var line in lines)
    {
        Log(line);
        var mifno = MediaInfo.ParseMediaUrl(line);
        if (mifno == null)
        {
            Log("MediaInfo.ParseMediaUrl returned null");
            continue;
        }
        string videoId = (string)mifno.Dynamic.videoId;
        EasyObject videoInfo = props.Get(videoId);
        if (videoInfo.IsNull)
        {
            Log($"No video info found for videoId: {videoId}");
            continue;
        }
        if (TextEmbedder.HasEmbeddedText(line))
        {
            Log("Embedded text already exists for this line. Skipping embedding.");
            continue;
        }
        TextEmbedder.InjectEmbeddedText(line, videoInfo.ToJson(indent: true, keyAsSymbol: true));
    }
    foreach (var line in lines)
    {
        Log(line);
        var text = TextEmbedder.ExtractEmbeddedText(line);
        Log(text);
    }
}
catch (Exception e)
{
    Crash(e);
}
