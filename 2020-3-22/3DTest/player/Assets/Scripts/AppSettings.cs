using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AppSettings {
    public static string DataDir = LitJson.JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/settings.json"))["dataDirectory"].ToString();
    public static string PlaylistSetsDirName = "playlist_sets/";
    public static string SystemSettingsFileName = "system_settings.json";
    public static string CurrentDirName = "current/";
    public static string CurrentLnkFileName = "current.lnk";
    public static string[] imageExtensions = { ".png", ".jpg", ".jpeg" };
    public static string[] movieExtensions = { ".mov", ".mp4" };
    public static string[] transitionExtensions = { ".txt" };
    public static string lnkExtension = ".lnk";
    public static string fileTypeImage = "image";
    public static string fileTypeMovie = "movie";
    public static string fileTypeTransition = "transition";
    public static string playlistJsonFileName = "playlists.json";
    public static string playlistSettingsJsonFileName = "settings.json";
    public static int ScreenWidth = 3840;
    public static int ScreenHeight = 2160;
}
