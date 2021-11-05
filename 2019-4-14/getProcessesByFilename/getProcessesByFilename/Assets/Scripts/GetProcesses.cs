using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProcesses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("aaa");
        //Debug.Log(GetProcessesByFileName("countUp_a.exe"));
        //Debug.Log("aaa2");

        //System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("countUp_a");
        //Debug.Log(ps);
        //foreach (System.Diagnostics.Process p in ps)
        //{
        //    Debug.Log("name = " + p.ProcessName + ", id = " + p.Id);
        //}

        string processName = "countUp_a";
        StartCoroutine(GetProcessIdsByProcessName(processName));
    }

    IEnumerator GetProcessIdsByProcessName(string _processName)
    {
        int imax = 36000;
        int ishow = 100;
        float initialTime = Time.realtimeSinceStartup;
        for (int i = 0; i<imax; i++)
        {
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(_processName);
            //Debug.Log(ps);
            if (i%ishow == 0)
            {
                foreach (System.Diagnostics.Process p in ps)
                {
                    Debug.Log(i + " name = " + p.ProcessName + ", id = " + p.Id);
                }
                float dt = Time.realtimeSinceStartup - initialTime;
                Debug.Log(i + ", dt = " + dt + "[s] = " + dt/60.0f + "[min]");
                yield return null;
            }
        }

    }

    //IEnumerator GetProcessesByFileNameIEnum()
    //{
    //    int imax = 1000;
    //    float initialTime = Time.realtimeSinceStartup;
    //    for (int i = 0; i < imax; i++)
    //    {
    //        float dt = Time.realtimeSinceStartup - initialTime;
    //        Debug.Log(i + ", dt = " + dt);
    //        GetProcessesByFileName("countUp_a.exe");

    //    }

    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public static System.Diagnostics.Process[] GetProcessesByFileName(string searchFileName)
    {
        searchFileName = searchFileName.ToLower();
        System.Collections.ArrayList list = new System.Collections.ArrayList();

        //すべてのプロセスを列挙する
        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
        {
            string fileName;
            try
            {
                //メインモジュールのパスを取得する
                fileName = p.MainModule.FileName;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                //MainModuleの取得に失敗
                fileName = "";
            }
            if (0 < fileName.Length)
            {
                //ファイル名の部分を取得する
                //Debug.Log("1 fileName " + fileName);
                fileName = System.IO.Path.GetFileName(fileName);
                //Debug.Log("2 fileName " + fileName);
                //探しているファイル名と一致した時、コレクションに追加
                if (searchFileName.Equals(fileName.ToLower()))
                {
                    //Debug.Log("p " + p);
                    //Debug.Log("p name " + p.ProcessName);
                    //Debug.Log("p id " + p.Id + ", p name " + p.ProcessName);
                    list.Add(p);
                }
            }
        }

        //コレクションを配列にして返す
        return (System.Diagnostics.Process[])
            list.ToArray(typeof(System.Diagnostics.Process));
    }
}
