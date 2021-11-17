using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSettings : MonoBehaviour
{
    public struct FrameStruct
    {
        public int x;
        public int y;
        public int width;
        public int height;
    }

    public struct LayoutStruct
    {
        public string name;
        public List<List<FrameStruct>> frames;
    }

    public struct AreaStruct
    {
        public string name;
        public int width;
        public int height;
        public List<Dictionary<string, FrameStruct>> onair;
        public int onair_label_x;
        public int onair_label_y;
        public int onair_label_size;
        public List<Dictionary<string, FrameStruct>> next;
        public int next_label_x;
        public int next_label_y;
        public int next_label_size;
    }


    public struct SettingsStruct
    {
        public List<AreaStruct> areas;
        public List<LayoutStruct> layouts;
    }

    public SettingsStruct Settings;

    // -----------------------------------------------------------------------------------------------------
    public void MakeSettings()
    {
        Settings = new SettingsStruct();
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetSettings(SettingsStruct _settingsStruct)
    {
        Settings = _settingsStruct;
    }

    //// -----------------------------------------------------------------------------------------------------
    //void Start()
    //{
    //}

    //// -----------------------------------------------------------------------------------------------------
    //void Update()
    //{
    //}
}
