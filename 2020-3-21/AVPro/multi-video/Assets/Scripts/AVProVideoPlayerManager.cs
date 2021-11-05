using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVProVideoPlayerManager : MonoBehaviour
{
    public GameObject AVProVideoPlayerPrefab;
    public GameObject ContentsPanel;
    public Dictionary<string, GameObject> VideoPlayersDic = new Dictionary<string, GameObject>();
    public string PlayingStatus = "stop";

    private int numOfVideosStart = 1;
    private int numOfVideosEnd   = 40+1;
    //private string fileDir = "D:/chart/video/random";
    private string fileDir = "D:/chart/video/dpx";

    void Start()
    {
        for(int i=numOfVideosStart; i<numOfVideosEnd; i++)
        {
            //MakeVideoPlayer("chart-random_" + i.ToString("00") + ".mov");
            MakeVideoPlayer("chart-dpx_hap_" + i.ToString("00") + ".mov");
            //MakeVideoPlayer("chart-dpx_h264_" + i.ToString("00") + ".mp4");
            //MakeVideoPlayer("chart-dpx_h265_" + i.ToString("00") + ".mp4");
        }
    }

    void Update()
    {
        DebugFunctionsByKeyDown();        
    }

    // -----------------------------------------------------------------------------------------------------
    void MakeVideoPlayer(string _videoFileName)
    {
        GameObject _videoAudioPlayerTmp = Instantiate(AVProVideoPlayerPrefab, this.transform) as GameObject;
        _videoAudioPlayerTmp.GetComponent<AVProVideoPlayer>().ContentsPanel = ContentsPanel;
        _videoAudioPlayerTmp.GetComponent<AVProVideoPlayer>().FileDir = fileDir;
        _videoAudioPlayerTmp.GetComponent<AVProVideoPlayer>().FileName = _videoFileName;
        _videoAudioPlayerTmp.GetComponent<AVProVideoPlayer>().SetVideo();
        _videoAudioPlayerTmp.GetComponent<AVProVideoPlayer>().SetLoopOn();
        VideoPlayersDic.Add(_videoFileName, _videoAudioPlayerTmp);
    }

    // -----------------------------------------------------------------------------------------------------
    void DebugFunctionsByKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayingStatus != "play")
            {
                PlayAll();
            }
            else
            {
                PauseAll();
            }
        }
    }

    // -----------------------------------------------------------------------------------------------------
    void PlayAll()
    {
        foreach (string _videoPlayer in VideoPlayersDic.Keys)
        {
            VideoPlayersDic[_videoPlayer].GetComponent<AVProVideoPlayer>().Play();
        }
        PlayingStatus = "play";
    }

    // -----------------------------------------------------------------------------------------------------
    void PauseAll()
    {
        foreach (string _videoPlayer in VideoPlayersDic.Keys)
        {
            VideoPlayersDic[_videoPlayer].GetComponent<AVProVideoPlayer>().Pause();
        }
        PlayingStatus = "pause";
    }

}
