using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{
    public GameObject GO;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        GO.SetActive(true);
    }

    public void Deactivate()
    {
        GO.SetActive(false);
    }

    public void SetActive()
    {
        Debug.Log(toggle.isOn);
        if (toggle.isOn)
        {
            GO.SetActive(true);
        }
        else
        {
            GO.SetActive(false);
        }
    }

}
