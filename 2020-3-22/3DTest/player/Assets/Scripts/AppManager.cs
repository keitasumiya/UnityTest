using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    // -----------------------------------------------------------------------------------------------------
    void Awake()
    {
        SettingsLoader _settingsLoader = gameObject.AddComponent<SettingsLoader>() as SettingsLoader;
        _settingsLoader.Set();
    }

    // -----------------------------------------------------------------------------------------------------
    void Start()
    {
        
    }

    // -----------------------------------------------------------------------------------------------------
    void Update()
    {
        
    }
}
