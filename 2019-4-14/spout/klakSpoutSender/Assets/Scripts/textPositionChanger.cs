using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textPositionChanger : MonoBehaviour
{
    public GameObject textGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonUp()
    {
        textGO.transform.Translate(0, 1, 0);
    }

    public void buttonDown()
    {
        textGO.transform.Translate(0, -1, 0);
    }

}
