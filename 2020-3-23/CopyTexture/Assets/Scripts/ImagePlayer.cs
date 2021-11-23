using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ImagePlayer : MonoBehaviour
{
    public Material ImageMaterial;

    private Texture2D imageTexture;
    private string logHead = "[ImagePlayer] ";

    // -----------------------------------------------------------------------------------------------------
    //void Start()
    //{
    //}

    // -----------------------------------------------------------------------------------------------------
    //void Update()
    //{
    //}

    // -----------------------------------------------------------------------------------------------------
    public void OpenMedia(string _targetFilePath)
    {
        imageTexture = ReadTexture(_targetFilePath);
        ImageMaterial.SetTexture("_MainTex", imageTexture);
        Debug.Log(logHead + "Opened");
    }

    // -----------------------------------------------------------------------------------------------------
    Texture2D ReadTexture(string path)
    {
        byte[] readBinary = ReadFile(path);

        //Texture2D texture = new Texture2D(1, 1);
        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        texture.LoadImage(readBinary);
        //Debug.Log(texture.width.ToString() + "  " + texture.height.ToString());

        return texture;
    }

    // -----------------------------------------------------------------------------------------------------
    byte[] ReadFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader bin = new BinaryReader(fileStream);
        byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

        bin.Close();

        return values;
    }

    // -----------------------------------------------------------------------------------------------------
    //public void SetAsFirstContent()
    //{
    //    Debug.Log(logHead + "Set this content as first content");
    //}

}
