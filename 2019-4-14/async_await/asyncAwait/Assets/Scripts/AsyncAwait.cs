using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
//using UniRx;

public class AsyncAwait : MonoBehaviour
{
    int count = 0;
    private void Start()
    {
        // 普通のメソッドみたいに呼び出せばOK
        //var _ = ExampleAsync();
        //var _ = Func("notepad");
        //foreach (System.Diagnostics.Process p in ps0)
        //{
        //    Debug.Log("[unnecessary process] name = " + p.ProcessName + ", id = " + p.Id);
        //}
        Debug.Log(Get("notepad"));
    }

    private void Update()
    {
        count++;
        //if (count%10 == 0)
        //{
        //    Debug.Log(count);
        //}
        if (count == 200)
        {
            Debug.Log(count);
            foreach (System.Diagnostics.Process p in ps0)
            {
                Debug.Log("[unnecessary process] name = " + p.ProcessName + ", id = " + p.Id);
            }
        }
    }

    private async Task ExampleAsync()
    {
        //Debug.Log("開始");
        //var url = "https://unity3d.com/jp/";
        //// await
        //var result = await Observable.FromCoroutine<string>(observer => GetCoroutine(url, observer));
        //// 結果がとれる
        //Debug.Log(result);

        await Task.Run(() => {
            Thread.Sleep(1000);
            DoHeavy();
            DoHeavyString();
            string hoge = DoHeavyString2();
            Debug.Log(hoge);
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("notepad"); // heveay
            Debug.Log("aaa");
            foreach (System.Diagnostics.Process p in ps)
            {
                Debug.Log("[unnecessary process] name = " + p.ProcessName + ", id = " + p.Id);
            }

            //string hoge2 = this.GetComponent<Test>().DoHeavyString();
            //Debug.Log(hoge2);
            //float _currentTime = Time.realtimeSinceStartup;
            //StartCoroutine(Hoge());
        });
        Debug.Log("終了");
    }

    private void DoHeavy()
    {
        Debug.Log("heavy");
    }

    private string DoHeavyString()
    {
        Debug.Log("heavy2");
        return "heavyyyy";
    }

    private string DoHeavyString2()
    {
        Debug.Log("heavy3");
        return "heavyyyy3";
    }

    IEnumerator Hoge()
    {
        Debug.Log("hogee1");
        yield return null;
        Debug.Log("hogee2");
    }


    async Task Func(string _processName)
    {
        System.Diagnostics.Process[] ps;
        await Task.Run(() => {
            ps = System.Diagnostics.Process.GetProcessesByName(_processName); // heveay
            foreach (System.Diagnostics.Process p in ps)
            {
                Debug.Log("[unnecessary process] name = " + p.ProcessName + ", id = " + p.Id);
            }
            return ps;
        });
    }

    private System.Diagnostics.Process[] ps0;
    async Task Get(string _processName)
    {
        await Task.Run(() => {
            ps0 = System.Diagnostics.Process.GetProcessesByName(_processName); // heveay
        });
    }

    //IEnumerator GetCoroutine(string url, IObserver<string> observer)
    //{
    //    var www = new WWW(url);

    //    yield return www;

    //    if (!string.IsNullOrEmpty(www.error))
    //    {
    //        //失敗
    //        observer.OnError(new Exception(www.error));
    //    }
    //    else
    //    {
    //        // 正常系
    //        observer.OnNext(www.text);
    //        observer.OnCompleted();
    //    }
    //}
}
