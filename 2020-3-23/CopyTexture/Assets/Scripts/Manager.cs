using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject CopyPrefab;
    private int iMax = 100;

    void Start()
    {
        for (int i = 0; i < iMax; i++)
        {
            Instantiate(CopyPrefab, this.transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
