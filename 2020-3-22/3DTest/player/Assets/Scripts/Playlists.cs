using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlists : MonoBehaviour
{
    public Dictionary<string, PlaylistStruct> PlaylistDic;

    public struct ContentStruct
    {
        public string path;
        public string targetFilePath;
        public string targetFileExt;
        public string targetFilenameWithoutExt;
        public string serialStr;
        public string name;
        public string durationMilliSecStr;
        public string extenstion;
        public string fileType;
    }

    public struct FrameStruct
    {
        public string path;
        public string directoryName;
        public string serialStr;
        public List<ContentStruct> contents;
    }

    public struct AreaStruct
    {
        public string path;
        public string directoryName;
        public string serialStr;
        public List<FrameStruct> frames;
    }

    public struct PlaylistStruct
    {
        public string path;
        public string directoryName;
        public string name;
        public string serialStr;
        public string layout;
        public string duration;
        public string comment;
        public List<AreaStruct> areas;
    }

    // -----------------------------------------------------------------------------------------------------
    public void MakePlaylistDic()
    {
        PlaylistDic = new Dictionary<string, PlaylistStruct>();
    }


    // -----------------------------------------------------------------------------------------------------
    public void AddPlaylist(string _key, PlaylistStruct _playlist)
    {
        PlaylistDic.Add(_key, _playlist);
    }


    // -----------------------------------------------------------------------------------------------------
    //void Start()
    //{
    //}

    // -----------------------------------------------------------------------------------------------------
    //void Update()
    //{
    //}
}
