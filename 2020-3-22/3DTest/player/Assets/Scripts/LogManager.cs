using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogManager : MonoBehaviour
{
    private string makeLogBatPath = Application.streamingAssetsPath + "/Scripts/makeLog.bat";
    private string deleteOldLogsBatPath = Application.streamingAssetsPath + "/Scripts/deleteOldLogs.bat";
    private string logDirPath = Application.streamingAssetsPath + "/../../logs";

    // -----------------------------------------------------------------------------------------------------
    void Awake()
    {
#if UNITY_EDITOR
#else
        CheckExistsAndCreateLogsDirectory();
#endif
        DeleteOldLogs();
        MakeMakeLogBat();
    }

    //// -----------------------------------------------------------------------------------------------------
    //void Start()
    //{
    //}

    //// -----------------------------------------------------------------------------------------------------
    //void Update()
    //{
    //}


    // -----------------------------------------------------------------------------------------------------
    private void OnDestroy()
    {
        Debug.Log("[LogManager] OnDestroy");
#if UNITY_EDITOR
#else
        DoBatWithoutConsole(makeLogBatPath);
        //string _directoryName = System.IO.Path.GetDirectoryName(makeLogBatPath);
        //System.Diagnostics.Process p = new System.Diagnostics.Process();
        //p.StartInfo.WorkingDirectory = _directoryName;
        //p.StartInfo.FileName = makeLogBatPath;
        //p.StartInfo.Arguments = "";
        //p.StartInfo.CreateNoWindow = true;
        //p.StartInfo.UseShellExecute = false;
        //p.Start();
#endif
    }


    // -----------------------------------------------------------------------------------------------------
    private void DeleteOldLogs()
    {
#if UNITY_EDITOR
#else
        Debug.Log("[LogManager] DeleteOldLogs");
        DoBatWithoutConsole(deleteOldLogsBatPath);
        //string _directoryName = System.IO.Path.GetDirectoryName(deleteOldLogsBatPath);
        //System.Diagnostics.Process p = new System.Diagnostics.Process();
        //p.StartInfo.WorkingDirectory = _directoryName;
        //p.StartInfo.FileName = deleteOldLogsBatPath;
        //p.StartInfo.Arguments = "";
        //p.StartInfo.CreateNoWindow = true;
        //p.StartInfo.UseShellExecute = false;
        //p.Start();
#endif
    }


    // -----------------------------------------------------------------------------------------------------
    void DoBatWithoutConsole(string _filePath)
    {
        string _directoryName = System.IO.Path.GetDirectoryName(_filePath);
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.WorkingDirectory = _directoryName;
        p.StartInfo.FileName = _filePath;
        p.StartInfo.Arguments = "";
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.UseShellExecute = false;
        p.Start();
    }


    // -----------------------------------------------------------------------------------------------------
    void CheckExistsAndCreateLogsDirectory()
    {
        if (!Directory.Exists(logDirPath))
        {
            Directory.CreateDirectory(logDirPath);
        }
    }


    // -----------------------------------------------------------------------------------------------------
    void MakeMakeLogBat()
    {
        string[] _batCodes = {
                "@REM This bat is launched when a player application is destroyed.",
                "@echo off",
                "set time2=%time: =0%",
                "set ulogfile=log_%date:~0,4%-%date:~5,2%-%date:~8,2%-%time2:~0,2%%time2:~3,2%.txt",
                "@REM echo %ulogfile%",
                "echo waiting for making a log file...",
                "timeout 2 >nul",
                "echo copying a log file",
                "copy \"C:\\Users\\%USERNAME%\\AppData\\LocalLow\\"+Application.companyName+"\\"+Application.productName+"\\Player.log\" %~dp0..\\..\\..\\logs\\%ulogfile%",
                "exit /b"
            };
        string _batCode = string.Join("\n", _batCodes);
        File.WriteAllText(makeLogBatPath, _batCode);
        Debug.Log("[LogManager] made a bat file to make a log");
    }


}
