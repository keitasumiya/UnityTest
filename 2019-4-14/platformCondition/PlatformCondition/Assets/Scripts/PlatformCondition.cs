using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            Debug.Log("unity editor");
        #else
            Debug.Log("other platform");
        #endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
