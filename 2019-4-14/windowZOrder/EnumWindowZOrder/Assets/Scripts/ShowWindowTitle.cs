using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWindowTitle : MonoBehaviour
{
    private static int windowIndex = 0;
    private static List<int> windowIndexes = new List<int>();

    private void Start()
    {
        EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        //Console.ReadLine();
        Debug.Log(windowIndexes[0]);
    }
    /// <summary>
    /// エントリポイント
    /// </summary>
    //public static void Main()
    //{
    //    //ウィンドウを列挙する
    //    EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);

    //    Console.ReadLine();
    //}

    public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc,
        IntPtr lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd,
        StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd,
        StringBuilder lpClassName, int nMaxCount);

    private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
    {
        //ウィンドウのタイトルの長さを取得する
        int textLen = GetWindowTextLength(hWnd);
        if (0 < textLen)
        {
            //ウィンドウのタイトルを取得する
            StringBuilder tsb = new StringBuilder(textLen + 1);
            GetWindowText(hWnd, tsb, tsb.Capacity);

            //ウィンドウのクラス名を取得する
            StringBuilder csb = new StringBuilder(256);
            GetClassName(hWnd, csb, csb.Capacity);

            //結果を表示する
            //Console.WriteLine("クラス名:" + csb.ToString());
            //Console.WriteLine("タイトル:" + tsb.ToString());
            windowIndex++;
            windowIndexes.Add(windowIndex);
            Debug.Log(windowIndex);
            Debug.Log(windowIndex.ToString() + " クラス名:" + csb.ToString());
            Debug.Log(windowIndex.ToString() + " タイトル:" + tsb.ToString());
        }

        //すべてのウィンドウを列挙する
        return true;
    }
}