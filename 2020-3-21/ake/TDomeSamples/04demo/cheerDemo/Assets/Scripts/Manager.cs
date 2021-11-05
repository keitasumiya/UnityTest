using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    //public GameObject sign01, sign02, sign03, sign04, sign05;

    public GameObject[] signs = new GameObject[5];
    public GameObject boxes, flags;
    private Vector3[] initialPositions = new Vector3[5];
    public float bpm = 120.0f;
    public int cheerMode = 0;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< 5; i++){
            initialPositions[i] = signs[i].transform.position;
        }
        startTime = Time.time;
        LightsSetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            cheerMode =0;
            boxes.SetActive(true);
            flags.SetActive(false);
            LightsSetActive(false);
            ResetPosition();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            startTime = Time.time;
        }

        if(      Input.GetKeyDown(KeyCode.Alpha1)){
            cheerMode = 1;
            boxes.SetActive(true);
            flags.SetActive(false);
            LightsSetActive(false);
            ResetPosition();
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            cheerMode = 2;
            boxes.SetActive(true);
            flags.SetActive(false);
            LightsSetActive(false);
            ResetPosition();
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            cheerMode = 3;
            boxes.SetActive(true);
            flags.SetActive(false);
            LightsSetActive(false);
            ResetPosition();
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            cheerMode = 4;
            boxes.SetActive(false);
            flags.SetActive(true);
            LightsSetActive(false);
        }else if(Input.GetKeyDown(KeyCode.Alpha5)){
            cheerMode = 5;
            boxes.SetActive(true);
            flags.SetActive(false);
            LightsSetActive(true);
        }



        if(cheerMode == 1){ // up and down
            for(int i=0; i< 5; i++){
                Vector3 pos = signs[i].transform.position;
                float fi = (float)i;
                float secondPerBeat = 0.5f;                
                if( bpm > 0.0f ) {
                    secondPerBeat = 60.0f / bpm;
                }
                float phase = (Time.time - startTime + fi/(secondPerBeat*10)) *  1.0f/secondPerBeat  * Mathf.PI;

                pos.y = Mathf.Sin(phase) * 0.1f + 0.05f;
                signs[i].transform.position = pos;

            }

        }
        
        if(cheerMode == 2){ // front and back
            for(int i=0; i< 5; i++){
                Vector3 pos = signs[i].transform.position;
                float fi = (float)i;
                float secondPerBeat = 0.5f;                
                if( bpm > 0.0f ) {
                    secondPerBeat = 60.0f / bpm;
                }
                float phase = (Time.time - startTime + fi/(secondPerBeat*10)) *  1.0f/secondPerBeat  * Mathf.PI;
                pos.z = Mathf.Sin(phase) * 0.3f + initialPositions[i].z;
                signs[i].transform.position = pos;
            }
        }

        if(cheerMode == 3){ // bright and 
            for(int i=0; i< 5; i++){
                Vector3 pos = signs[i].transform.position;
                float fi = (float)i;
                float secondPerBeat = 0.5f;                
                if( bpm > 0.0f ) {
                    secondPerBeat = 60.0f / bpm;
                }
                float phase = (Time.time - startTime + fi/(secondPerBeat*10)) *  1.0f/secondPerBeat  * Mathf.PI;
                Color col = Color.white;
                col.r = Mathf.Sin(phase)* 0.5f  + 0.8f;
                col.g = col.r;
                col.b = col.r;
                signs[i].GetComponent<Renderer>().material.SetColor("_Color", col);
            }
        }
        if(cheerMode == 5){ // light circler
            for(int i=0; i< 5; i++){
                float secondPerBeat = 0.5f;                
                //float phase = (Time.time - startTime + (float)i/(secondPerBeat*10)) *  1.0f/secondPerBeat  * Mathf.PI;
                float phase = (Time.time - startTime + (float)i/(secondPerBeat*10)) *  1.0f/secondPerBeat;
                Vector2 pos = MapAround(phase % 1);                
                //Vector2 pos = MapAround(Mathf.Sin(phase) * 0.5f + 0.5f);
                Vector3 lightPos = signs[i].transform.Find("PointLight").gameObject.transform.localPosition;
                lightPos.x = pos.x;
                lightPos.y = pos.y;
                signs[i].transform.Find("PointLight").gameObject.transform.localPosition = lightPos;
            //    signs[i].transform.Find("PointLight").gameObject.GetComponent<Light>().intensity = Random.Range(3.0f, 10.0f);
            }    
        }
    }
    
    Vector2 MapAround(float v){
        // -0.44, 0.44 -> 0.44, 0.44 -> 0.44, -0.44 -> -0.44, -0.44 -> -0.44, 0.44
        // 0 : -1, 1
        // 0.25 : 1, 1
        // 0.5 : 1, -1
        // 0.75: -1, -1
        // 1.0 : -1, 1
        Vector2 r = new Vector2(0.0f,0.0f);
        if(v >= 0.0f && v < 0.25f){
            r.x =  Map(v, 0.0f, 0.25f, -1.0f, 1.0f);
            r.y = 1.0f;
        }else if(v >= 0.25f && v < 0.5f){
            r.x =  1.0f;
            r.y = Map(v, 0.25f, 0.5f, 1.0f, -1.0f);
        }else if(v >= 0.5f && v < 0.75f){
            r.x = Map(v, 0.5f, 0.75f, 1.0f, -1.0f);
            r.y = -1.0f;
        }else if(v >= 0.75f && v <= 1.0f){
            r.x =  -1.0f;
            r.y = Map(v, 0.75f, 1.0f, -1.0f, 1.0f);
        }

        r.x *= 0.44f;
        r.y *= 0.44f * (640.0f / 768.0f);

        return r;
    }
    public static float Map (float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    void LightsSetActive(bool active){
        for(int i=0; i< 5; i++){
            signs[i].transform.Find("PointLight").gameObject.SetActive(active);
        }
    }

    void ResetPosition(){
        for(int i=0; i< 5; i++){
            signs[i].transform.position = initialPositions[i];
            signs[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

    }


}