using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;
using System.Linq;

public class SettingsLoader : MonoBehaviour
{
    private Playlists playlistsInfo;
    private SystemSettings systemSettingsInfo;


    //// -----------------------------------------------------------------------------------------------------
    //void Awake()
    //{
    //}


    //// -----------------------------------------------------------------------------------------------------
    //void Start()
    //{
    //}


    //// -----------------------------------------------------------------------------------------------------
    //void Update()
    //{
    //}

    // -----------------------------------------------------------------------------------------------------
    public void Set()
    {
        playlistsInfo = gameObject.AddComponent<Playlists>() as Playlists;
        playlistsInfo.MakePlaylistDic();
        systemSettingsInfo = gameObject.AddComponent<SystemSettings>() as SystemSettings;
        systemSettingsInfo.MakeSettings();

        Debug.Log("[SettingsLoader] " + AppSettings.DataDir);
        analyzeSystemLayoutSettings();
        getCurrentPlaylistSetsInfo();
    }


    // -----------------------------------------------------------------------------------------------------
    void analyzeSystemLayoutSettings()
    {
        SystemSettings.SettingsStruct _settings = LitJson.JsonMapper.ToObject<SystemSettings.SettingsStruct>(System.IO.File.ReadAllText(AppSettings.DataDir + AppSettings.SystemSettingsFileName));
        systemSettingsInfo.SetSettings(_settings);
    }


    // -----------------------------------------------------------------------------------------------------
    void getCurrentPlaylistSetsInfo()
    {
        string _currentDirPath = checkCurrentExistenceAndGetCurrent();
        Debug.Log("[SettingsLoader] " + _currentDirPath);
        if (_currentDirPath != "")
        {
            string[] _playlistDirs = Directory.GetDirectories(_currentDirPath);
            foreach (string _playlistDir in _playlistDirs)
            {
                int _errorCount = 0;
                Debug.Log(_playlistDir);
                Playlists.PlaylistStruct _playlistTmp = new Playlists.PlaylistStruct();
                _playlistTmp.path = _playlistDir;
                _playlistTmp.directoryName = Path.GetFileName(_playlistDir);
                string[] _playlistComponents = _playlistTmp.directoryName.Split('_');
                if (_playlistComponents.Length != 2 || !System.IO.File.Exists(_playlistDir + "/" + AppSettings.playlistSettingsJsonFileName))
                {
                    _errorCount += 1;
                    Debug.Log("[SettingsLoader] invalid playlist name or missing settings.json" + _playlistDir);
                }
                else
                {
                    _playlistTmp.serialStr = _playlistComponents[0];
                    _playlistTmp.name = _playlistComponents[1];
                    _playlistTmp.areas = new List<Playlists.AreaStruct>();
                    LitJson.JsonData _playlistSettingsJson = LitJson.JsonMapper.ToObject(System.IO.File.ReadAllText(_playlistDir + "/" + AppSettings.playlistSettingsJsonFileName));
                    _playlistTmp.layout = _playlistSettingsJson["layout"].ToString();
                    _playlistTmp.duration = _playlistSettingsJson["duration"].ToString();
                    _playlistTmp.comment = _playlistSettingsJson["comment"].ToString();

                    string[] _areaDirs = Directory.GetDirectories(_playlistDir);
                    foreach (string _areaDir in _areaDirs)
                    {
                        Debug.Log(_areaDir);
                        Playlists.AreaStruct _areaTmp = new Playlists.AreaStruct();
                        _areaTmp.path = _areaDir;
                        _areaTmp.directoryName = Path.GetFileName(_areaDir);
                        _areaTmp.serialStr = Path.GetFileName(_areaDir).Replace("area", "");
                        _areaTmp.frames = new List<Playlists.FrameStruct>();

                        string[] _frameDirs = Directory.GetDirectories(_areaDir);
                        int layoutID = int.Parse(_playlistTmp.layout);
                        int areaID = int.Parse(_areaTmp.serialStr);
                        int _frameNumSetting = systemSettingsInfo.Settings.layouts[layoutID].frames[areaID].Count;
                        Debug.Log(_frameNumSetting);
                        if (_frameDirs.Length == _frameNumSetting)
                        {
                            foreach (string _frameDir in _frameDirs)
                            {
                                Debug.Log(_frameDir);
                                Playlists.FrameStruct _frameTmp = new Playlists.FrameStruct();
                                _frameTmp.path = _frameDir;
                                _frameTmp.directoryName = Path.GetFileName(_frameDir);
                                _frameTmp.serialStr = Path.GetFileName(_frameDir).Replace("frame", "");
                                _frameTmp.contents = new List<Playlists.ContentStruct>();
                                string[] _contents = Directory.GetFiles(_frameDir);
                                foreach (string _content in _contents)
                                {
                                    Playlists.ContentStruct _contentTmp = new Playlists.ContentStruct();
                                    _contentTmp.path = _content;
                                    _contentTmp = insertContentPath(_contentTmp);
                                    _contentTmp = checkAndInsertContentInfo(_contentTmp);
                                    _frameTmp.contents.Add(_contentTmp);
                                }
                                _areaTmp.frames.Add(_frameTmp);
                            }
                            _playlistTmp.areas.Add(_areaTmp);
                        }
                        else
                        {
                            _errorCount += 1;
                            Debug.Log("[SettingsLoarder] frame layout is invalid : " + _areaDir);
                        }
                    }

                    if (_errorCount == 0)
                    {
                        playlistsInfo.AddPlaylist(_playlistTmp.serialStr, _playlistTmp);
                    }
                }
            }
            Debug.Log("playlistsInfo.PlaylistDic.Count = " + playlistsInfo.PlaylistDic.Count.ToString());
            string _playlistJsonStr = LitJson.JsonMapper.ToJson(playlistsInfo.PlaylistDic);
            WriteJSONFile(_playlistJsonStr, Application.streamingAssetsPath + "/" + AppSettings.playlistJsonFileName);
        }
    }

    // -----------------------------------------------------------------------------------------------------
    Playlists.ContentStruct insertContentPath(Playlists.ContentStruct _contentTmp)
    {
        string _filenameWithoutExt = Path.GetFileNameWithoutExtension(_contentTmp.path);
        string _fileExt = Path.GetExtension(_contentTmp.path).ToLower();

        string _targetFilePath = _contentTmp.path;
        string _targetFileExt = _fileExt;
        string _targetFilenameWithoutExt = _filenameWithoutExt;
        if (_fileExt == AppSettings.lnkExtension)
        {
            _targetFilePath = getTargetPath(_contentTmp.path);
            _targetFileExt = Path.GetExtension(_filenameWithoutExt).ToLower();
            _targetFilenameWithoutExt = Path.GetFileNameWithoutExtension(_filenameWithoutExt);
        }
        _contentTmp.targetFilePath = _targetFilePath;
        _contentTmp.targetFileExt = _targetFileExt;
        _contentTmp.targetFilenameWithoutExt = _targetFilenameWithoutExt;

        return _contentTmp;
    }

    // -----------------------------------------------------------------------------------------------------
    Playlists.ContentStruct checkAndInsertContentInfo(Playlists.ContentStruct _contentTmp)
    {
        string[] _targetFileComponents = _contentTmp.targetFilenameWithoutExt.Split('_');

        if (AppSettings.imageExtensions.Contains(_contentTmp.targetFileExt))
        {
            _contentTmp.fileType = AppSettings.fileTypeImage;
            if (_targetFileComponents.Length == 3)
            {
                _contentTmp = insertContentInfo(_contentTmp);
            }
            else { Debug.Log("[SettingsLoarder] filename is invalid : " + _contentTmp.path); }
        }
        else if (AppSettings.movieExtensions.Contains(_contentTmp.targetFileExt))
        {
            _contentTmp.fileType = AppSettings.fileTypeMovie;
            if (_targetFileComponents.Length == 2 || _targetFileComponents.Length == 3)
            {
                _contentTmp = insertContentInfo(_contentTmp);
            }
            else { Debug.Log("[SettingsLoarder] filename is invalid : " + _contentTmp.path); }
        }
        else if (AppSettings.transitionExtensions.Contains(_contentTmp.targetFileExt))
        {
            _contentTmp.fileType = AppSettings.fileTypeTransition;
            if (_targetFileComponents.Length == 3)
            {
                _contentTmp = insertContentInfo(_contentTmp);
            }
            else { Debug.Log("[SettingsLoarder] filename is invalid : " + _contentTmp.path); }
        }
        else
        {
            Debug.Log("[SettingsLoarder] file extenstion is invalid : " + _contentTmp.path);
        }

        return _contentTmp;
    }


    // -----------------------------------------------------------------------------------------------------
    Playlists.ContentStruct insertContentInfo(Playlists.ContentStruct _contentTmp)
    {
        string[] _targetFileComponents = _contentTmp.targetFilenameWithoutExt.Split('_');

        _contentTmp.serialStr = _targetFileComponents[0];
        _contentTmp.name = _targetFileComponents[1];
        if (_contentTmp.fileType != AppSettings.fileTypeMovie)
        {
            _contentTmp.durationMilliSecStr = _targetFileComponents[2].Replace("ms", "");
        }

        return _contentTmp;
    }


    // -----------------------------------------------------------------------------------------------------
    string checkCurrentExistenceAndGetCurrent()
    {
        string _playlistSetsDirPath = AppSettings.DataDir + AppSettings.PlaylistSetsDirName;
        string _currentPlaylistDirPath = _playlistSetsDirPath + AppSettings.CurrentDirName;
        string _currentPlaylistLnkFilePath = _playlistSetsDirPath + AppSettings.CurrentLnkFileName;
        string _resultCurrentPlaylistDirPath = "";


        if (Directory.Exists(_currentPlaylistDirPath))
        {
            if (System.IO.File.Exists(_currentPlaylistLnkFilePath))
            {
                _resultCurrentPlaylistDirPath = getTargetPath(_currentPlaylistLnkFilePath) + "/";
                Debug.Log("[SettingsLoader] current dir and current.lnk exist. set current as current.lnk");
            }
            else
            {
                _resultCurrentPlaylistDirPath = _currentPlaylistDirPath;
                Debug.Log("[SettingsLoader] current dir exists.");
            }
        }
        else
        {
            if (System.IO.File.Exists(_currentPlaylistLnkFilePath))
            {
                _resultCurrentPlaylistDirPath = getTargetPath(_currentPlaylistLnkFilePath) + "/";
                Debug.Log("[SettingsLoader] current.lnk exists. set current as current.lnk");
            }
            else
            {
                Debug.Log("[SettingsLoader] current dir and current.lnk dont exist. couldnt set current");
            }
        }
        return _resultCurrentPlaylistDirPath;
    }


    // -----------------------------------------------------------------------------------------------------
    string getTargetPath(string _path)
    {
        var shell = new WshShell();
        var shortcut = (IWshShortcut)shell.CreateShortcut(_path);
        return shortcut.TargetPath.Replace("\\", "/");
    }

    // -----------------------------------------------------------------------------------------------------
    void WriteJSONFile(string _json, string _path)
    {
        System.IO.File.WriteAllText(_path, _json);
    }

}
