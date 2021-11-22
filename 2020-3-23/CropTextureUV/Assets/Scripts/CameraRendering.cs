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

    int xMax = 10;
    int yMax = 10;
    List<GameObject> quads = new List<GameObject>();

    // --------------------------------------------------------
    void Start()
    {
        GameObject _camera = Instantiate(CameraPrefab, Cameras.transform) as GameObject;
        RenderTexture _rt = new RenderTexture(CameraRTOrigin);

        for (int j = 0; j < yMax; j++)
        {
            for (int i = 0; i < xMax; i++)
            {
                GameObject _quad = Instantiate(QuadPrefab, Quads.transform) as GameObject;
                _quad.transform.localPosition = new Vector3(i - xMax / 2.0f, j - yMax / 2.0f + 2.0f, 0.0f);
                Material _mat = new Material(CameraRTMatOrigin);
                _quad.name = "quad" + i.ToString() + "-" + j.ToString();
                _mat.name = "mat" + i.ToString() + "-" + j.ToString();
                _camera.GetComponent<Camera>().targetTexture = _rt;
                _mat.SetTexture("_MainTex", _rt);
                SetUV(_mat, i, j);
                _quad.GetComponent<MeshRenderer>().material = _mat;
                quads.Add(_quad);
            }
        }
    }

    // --------------------------------------------------------
    void SetUV(Material _mat, int i, int j)
    {
        float dx = 1.0f / xMax;
        float dy = 1.0f / yMax;
        _mat.SetTextureScale("_MainTex", new Vector2(dx, dy));
        _mat.SetTextureOffset("_MainTex", new Vector2(dx * i, dy * j));
    }
}
