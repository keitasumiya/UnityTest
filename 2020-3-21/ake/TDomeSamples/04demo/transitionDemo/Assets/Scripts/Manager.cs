using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Manager : MonoBehaviour
{
    public GameObject SignA, SignB;
    public GameObject Plate;
    public int mode = 0;
    public float duration = 1.0f;
    private float startTime;
    private Vector3 startPosition, endPosition;
    private Vector3 SignAInitialPosition;
    private Quaternion PlateInitialQuaternion;
        

    // Start is called before the first frame update
    void Start()
    {
        SignAInitialPosition = SignA.transform.position;    
        PlateInitialQuaternion = Plate.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            mode = 0;
            Reset();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Reset();            
            Vector3 p = SignAInitialPosition;
            p.z = -0.0f;
            endPosition = p;
            Vector3 s = SignA.transform.localScale;
            p.y = -s.y; // move to down
            SignB.transform.position = p;
            SignB.transform.DOMove(endPosition, duration).SetEase(Ease.InOutQuart);
        }

       if(Input.GetKeyDown(KeyCode.Alpha2)){
            Reset();            
            Vector3 p = SignAInitialPosition;
            Debug.Log(p);
            p.z = -0.0f;
            endPosition = p;
            Vector3 s = SignA.transform.localScale;
            Debug.Log(s);
            Debug.Log(s.y);
            p.y = s.y; // move to up
            Debug.Log(p);
            SignB.transform.position = p;
            SignB.transform.DOMove(endPosition, duration).SetEase(Ease.InOutQuart);
        }

       if(Input.GetKeyDown(KeyCode.Alpha3)){
            Reset();            
            Vector3 p = SignAInitialPosition;
            p.z = 0.0f;
            endPosition = p;
            Vector3 s = SignA.transform.localScale;
            p.y = -s.y; // move to down
            SignB.transform.position = p;
            SignB.transform.DOMove(endPosition, duration).SetEase(Ease.InOutQuart);

            Vector3 signAEndPosition = SignAInitialPosition;
            signAEndPosition.y = s.y; // move to up;
            SignA.transform.DOMove(signAEndPosition, duration).SetEase(Ease.InOutQuart);

        }

       if(Input.GetKeyDown(KeyCode.Alpha4)){
            Reset();            
            Vector3 p = SignAInitialPosition;
            p.z = 0.0f;
            Color col = Color.white;
            col.a = 0f;
            SignB.GetComponent<Renderer>().material.SetColor("_Color", col);
            SignB.GetComponent<Renderer>().material.DOFade(1.0f, duration);
            SignB.transform.position = p;
        }

       if(Input.GetKeyDown(KeyCode.Alpha5)){
            Reset();            
            mode = 5;
            SignA.GetComponent<Renderer>().material.DOColor(Color.black, duration/2.0f);
            SignB.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            SignB.GetComponent<Renderer>().material.DOColor( Color.white, duration/2.0f).SetDelay(duration/2.0f);            
            startTime = Time.time;        
        }

        if (mode == 5)
        {
            Vector3 p = SignAInitialPosition;
            p.z = 0.0f;
            float progress = (Time.time - startTime) / duration;
            if (progress > 0.5f)
            {
                SignB.transform.position = p;
                mode = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)){
           Reset();
           SignA.SetActive(false);
           SignB.SetActive(false);
           Plate.SetActive(true);
           Plate.transform.DORotate(new Vector3(0, 180, 0), 1, RotateMode.LocalAxisAdd);
       }

        if(Input.GetKeyDown(KeyCode.Alpha9)){
            Reset();            
            mode = 9;
            Vector3 p = SignA.transform.position;
            p.z = -0.0f;
            endPosition = p;
            Vector3 s = SignA.transform.localScale;
            p.y = s.y; // move to up
            startPosition = p;
            SignB.transform.position = p;
            startTime = Time.time;
        }

      if(mode == 9){
            float progress = (Time.time - startTime) / duration;
            Debug.Log(progress);
            if( progress < 1.0f){
                float yProgress = (endPosition.y - startPosition.y) * progress;
                Vector3 p = startPosition;
                p.y = p.y + yProgress;
                SignB.transform.position = p;
            }
        }


                

    }

    void Reset(){
        SignA.SetActive(true);
        SignB.SetActive(true);
        Vector3 p = SignA.transform.position;
        p.z = 0.02f;
        SignB.transform.position = p;
        SignA.transform.position = SignAInitialPosition;
        SignA.GetComponent<Renderer>().material.SetColor("_Color", Color.white);        
        SignA.GetComponent<Renderer>().material.SetColor("_Color", Color.white);    
        Plate.SetActive(false);
        Plate.transform.rotation = PlateInitialQuaternion;
    }
}


             