using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;

public class getTargetPath : MonoBehaviour
{
    void Start()
    {
        //var path = "C:\\Users\\ks\\Documents\\CSharpTest\\ConsoleAppTest\\Notepad.lnk";
        //var path = "C:\Users\ks\Documents\CSharpTest\ConsoleAppTest\Notepad.lnk";
        //var path = "C:/Users/ks/Documents/CSharpTest/ConsoleAppTest/Notepad.lnk";
        var path = "C:/board/playlist_sets/current/0000_black/area00/frame000/02_0001_10000ms.png.lnk";
        var shell = new WshShell();
        var shortcut = (IWshShortcut)shell.CreateShortcut(path);
        Debug.Log("image");
        Debug.Log(shortcut.TargetPath);

        //path = "C:/board/playlist_sets/current/playlist1/area1/frame1/chart-dpx_h264_01.mp4 - ショートカット.lnk";
        //path = "C:/board/playlist_sets/current/playlist1/area1/frame1/chart-dpx_h264_01.mp4.lnk";
        //shortcut = (IWshShortcut)shell.CreateShortcut(path);
        //Debug.Log("video");
        //Debug.Log(shortcut.TargetPath);

        //Console.WriteLine(shortcut.TargetPath);
        //Console.WriteLine("Press Enter to end...");
        //Console.ReadLine();

    }

    void Update()
    {
        
    }
}
