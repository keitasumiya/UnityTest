using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRendering : MonoBehaviour
{
    public Material CameraRTMatOrigin;
    public RenderTexture CameraRTOrigin;
    public GameObject CameraPrefab;
    public GameObject QuadPrefab;
    public GameObject Cameras;
    public GameObject Quads;

    int xMax = 2;
    int yMax = 2;
    List<GameObject> cameras = new List<GameObject>();
    List<GameObject> quads = new List<GameObject>();
    RenderTexture rt;
    Material mat;
    //Material[] mats = { };

    // --------------------------------------------------------
    void Start()
    {
        GameObject _camera = Instantiate(CameraPrefab, Cameras.transform) as GameObject;
        RenderTexture rt = new RenderTexture(CameraRTOrigin);
        mat = new Material(CameraRTMatOrigin);

        for (int j = 0; j < yMax; j++)
        {
            for (int i = 0; i < xMax; i++)
            {
                GameObject _quad = Instantiate(QuadPrefab, Quads.transform) as GameObject;
                _quad.transform.localPosition = new Vector3(i - xMax / 2.0f, j - yMax / 2.0f + 2.0f, 0.0f);
                Material _mat = new Material(CameraRTMatOrigin);
                _quad.name = "quad" + i.ToString() + "-" + j.ToString();
                _mat.name = "mat" + i.ToString() + "-" + j.ToString();
                _camera.GetComponent<Camera>().targetTexture = rt;
                _mat.SetTexture("_MainTex", CropTexture(rt));
                //SetUV(_mat, i, j);
                _quad.GetComponent<MeshRenderer>().material = _mat;
                quads.Add(_quad);
                cameras.Add(_camera);
            }
        }
    }

    void Update()
    {
        int count = 0;
        for (int j = 0; j < yMax; j++)
        {
            for (int i = 0; i < xMax; i++)
            {
                quads[count].GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_MainTex", cameras[count].GetComponent<Camera>().targetTexture);
                ////mat.SetTexture("_MainTex", cropTexture(MatInput));
                ////Material[] mats = { MatOutput };
                ////this.gameObject.GetComponent<MeshRenderer>().materials = mats;


                ////Debug.Log(quads[count].GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_MainTex", CropTexture(rt)));
                ////quads[count].GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_MainTex", rt);
                //mat.SetTexture("_MainTex", rt);
                //Material[] mats = { mat };
                //quads[count].GetComponent<MeshRenderer>().materials = mats;
                ////Material[] mats = { MatOutput };
                ////this.gameObject.GetComponent<MeshRenderer>().materials = mats;
                count++;
            }
        }
    }


    Texture CropTexture(Texture _texture)
    {
        Texture2D texture2D = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(_texture.width, _texture.height, 32);
        Graphics.Blit(_texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;


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
