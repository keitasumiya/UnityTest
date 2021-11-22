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
    int yMax = 3;

    // --------------------------------------------------------
    void Start()
    {
        for (int j = 0; j < yMax; j++)
        {
            for (int i = 0; i < xMax; i++)
            {
                GameObject _camera = Instantiate(CameraPrefab, Cameras.transform) as GameObject;
                GameObject _quad = Instantiate(QuadPrefab, Quads.transform) as GameObject;
                _quad.transform.localPosition = new Vector3(i - xMax / 2.0f, j + 5.0f, 0.0f);
                RenderTexture _rt = new RenderTexture(CameraRTOrigin);
                Material _mat = new Material(CameraRTMatOrigin);
                _camera.GetComponent<Camera>().targetTexture = _rt;
                _mat.SetTexture("_MainTex", _rt);
                _quad.GetComponent<MeshRenderer>().material = _mat;
            }
        }
    }

    // --------------------------------------------------------
    void Update()
    {
        
    }
}
