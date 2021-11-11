using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTexture : MonoBehaviour
{
    public Material MatInput;
    public Material MatOutput;

    void Start()
    {
        crop();
    }

    void Update()
    {
        crop();
    }

    void crop()
    {
        MatOutput.SetTexture("_MainTex", cropTexture(MatInput));
        Material[] mats = { MatOutput };
        this.gameObject.GetComponent<MeshRenderer>().materials = mats;
    }


    Texture cropTexture(Material mat)
    {
        Texture _texture = mat.GetTexture("_MainTex"); // Material のメインテクスチャを取得
        Texture2D texture2D = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(_texture.width, _texture.height, 32);
        // mainTexture のピクセル情報を renderTexture にコピー
        Graphics.Blit(_texture, renderTexture);

        //// renderTexture のピクセル情報を元に texture2D のピクセル情報を作成
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();


        Color[] pixel;
        Texture2D clipTex;

        int textureWidth = texture2D.width;
        int textureHeight = texture2D.height;

        int x = textureWidth / 2;
        int y = textureHeight / 2;
        int w = textureWidth / 2;
        int h = textureHeight / 2;
        pixel = texture2D.GetPixels(x, y, w, h);
        clipTex = new Texture2D(w, h);
        clipTex.SetPixels(pixel);
        clipTex.Apply();
        return clipTex;
    }
}
