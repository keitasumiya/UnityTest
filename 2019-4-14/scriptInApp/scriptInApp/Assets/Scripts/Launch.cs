using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launch : MonoBehaviour
{
    //int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        string FilePath;
#if UNITY_EDITOR
        FilePath = System.IO.Directory.GetCurrentDirectory();//Editor上では普通にカレントディレクトリを確認
#else
        FilePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');//EXEを実行したカレントディレクトリ (ショートカット等でカレントディレクトリが変わるのでこの方式で)
#endif
        Debug.Log(FilePath);
        Debug.Log(Application.streamingAssetsPath);
        //System.Diagnostics.Process.Start(Application.streamingAssetsPath + "/test.bat");

        //System.Diagnostics.Process.Start(Application.streamingAssetsPath + "/hoge/huga/test3.bat");

        string _fileFullPath = Application.streamingAssetsPath + "/hoge/huga/test3.bat";
        string _directoryName = System.IO.Path.GetDirectoryName(_fileFullPath);
        //Debug.Log("[SwApp] file dir : " + FileDir);
        //Debug.Log("[SwApp] change file name : " + _changedFileName);
        Debug.Log("[SwApp] full path : " + _fileFullPath);
        Debug.Log("[SwApp] directory : " + _directoryName);
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = _fileFullPath;
        p.StartInfo.Arguments = "";
        p.StartInfo.WorkingDirectory = _directoryName;
        p.Start();

        //System.Diagnostics.Process p = System.Diagnostics.Process.Start(Application.streamingAssetsPath + "/test.bat");
        //#if UNITY_STANDALONE_WIN
        //        System.Diagnostics.Process.Start(Application.streamingAssetsPath + "/test.bat");
        //        Application.Quit();
        //#endif
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        //count++;
        //Debug.Log(count);
    }
}
