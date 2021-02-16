using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionChanger : MonoBehaviour
{
    public GameObject Text;
    
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
        Text.transform.Translate(0, 10, 0);
    }

    public void buttonDown()
    {
        Text.transform.Translate(0, -10, 0);
    }
}
