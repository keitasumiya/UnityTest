using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenCapturer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //var app = new System.Diagnostics.ProcessStartInfo();
            //app.FileName = "C:/sbsc/data/script/boxcutter/boxcutter.exe";
            //app.Arguments = "-c 0,0,100,100 C:/sbsc/img.jpg";
            //app.UseShellExecute = true;
            //System.Diagnostics.Process.Start(app);
            //System.Diagnostics.Process.Start("C:/sbsc/data/script/boxcutter/boxcutter.exe", "-f C:/sbsc/img.jpg");
            System.Diagnostics.Process.Start("C:/Users/ks/Documents/WindowsTest/screencaputure/screencaputure.exe", "C:/Users/ks/Documents/WindowsTest/screencaputure/img.png");
            System.Diagnostics.Process.Start("C:/Users/ks/Documents/WindowsTest/screencaputure/screencaputure.exe", "C:/Users/ks/Documents/WindowsTest/screencaputure/img.png");

            //ScreenCapture.CaptureScreenshot("image.png");
            Debug.Log("screenshot");
        }
    }
}
