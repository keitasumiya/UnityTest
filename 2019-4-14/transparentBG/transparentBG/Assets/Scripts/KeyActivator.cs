using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActivator : MonoBehaviour
{
    public GameObject GO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GO.activeSelf)
            {
                GO.SetActive(false);
            }
            else
            {
                GO.SetActive(true);
            }
        }
        
    }
}
