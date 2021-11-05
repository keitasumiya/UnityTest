using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchPanel : MonoBehaviour
{
    GameObject child;
    GameObject parent1;
    GameObject parent2;
    GameObject escape;
    string who = "parent1";
    // Start is called before the first frame update
    void Start()
    {
        child = this.transform.Find("Parent1/child").gameObject;
        parent1 = this.transform.Find("Parent1").gameObject;
        parent2 = this.transform.Find("Parent2").gameObject;
        escape = this.transform.Find("../Escape").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        DebugFunctionsByKeyDown();
    }

    // -----------------------------------------------------------------------------------------------------
    void DebugFunctionsByKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (who == "parent1")
            {
                child.transform.SetParent(parent2.transform, false);
                who = "parent2";
            }
            else
            {
                child.transform.SetParent(parent1.transform, false);
                who = "parent1";
            }
            child.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (child.activeSelf)
            {
                child.SetActive(false);
                child.transform.SetParent(escape.transform, false);
            }
            else
            {
                child.SetActive(true);
            }
        }
    }



}
