using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class AVProVideoPlayer : MonoBehaviour
{
    public MediaPlayer MediaPlayerPrefab;
    public GameObject VideoPanelPrefab;
    public GameObject VideoPanel;
    public GameObject ContentsPanel;
    public string FileDir;
    public string FileName;
    public double CurrentVideoTimeSec;
    //public int displayIndex;

    private MediaPathType _location = MediaPathType.AbsolutePathOrURL;
    private MediaPlayer videoPlayer;
    private DisplayUGUI display;
    private bool canVideoPlay = false;
    private bool isFileReady = false;
    private double videoDurationSec;
    private string playingStatus = "stop";

    private float deltaFade;
    private float fadeInDurationSec = 2.0f;
    private float fadeOutDurationSec = 2.0f;
    private float fadeAlphaMin = 0;
    private float fadeAlphaMax = 1;
    private string fadeState = "none";
    private float fadeSpentTimeSec = 0;


    // -----------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (!isFileReady)
        {
            if (canVideoPlay)
            {
                //videoDurationSec = videoPlayer.Info.GetDuration() / 1000.0f;
                videoDurationSec = videoPlayer.Info.GetDuration();
                isFileReady = true;
            }
        }
        else
        {
            //CurrentVideoTimeSec = videoPlayer.Control.GetCurrentTime() / 1000.0f;
            CurrentVideoTimeSec = videoPlayer.Control.GetCurrentTime();
            if (playingStatus == "play")
            {
                DoFading();
            }

            //DebugFunctionsByKeyDown();
        }
    }

    // -----------------------------------------------------------------------------------------------------
    void DebugFunctionsByKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playingStatus != "play")
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartFadingOut();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartFadingIn();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CancelFading();
        }
    }

    // -----------------------------------------------------------------------------------------------------
    public void Play()
    {
        videoPlayer.Play();
        playingStatus = "play";
        string _log = "[" + playingStatus + "] : video : " + FileName;
        //Debug.Log(_log); //comment out for release
    }

    // -----------------------------------------------------------------------------------------------------
    public void PrePlay()
    {
        videoPlayer.Play();
    }

    // -----------------------------------------------------------------------------------------------------
    public void PlayFromSec(float _startTimeSec)
    {
        float _startTimeMilliSec = _startTimeSec * 1000.0f;
        videoPlayer.Control.Seek(_startTimeMilliSec);
        videoPlayer.Play();
        playingStatus = "play";
        string _log = "[" + playingStatus + "From] : video : " + _startTimeSec.ToString("f1") + " : " + FileName;
        //Debug.Log(_log); //comment out for release
    }

    // -----------------------------------------------------------------------------------------------------
    public void Pause()
    {
        videoPlayer.Pause();
        playingStatus = "pause";
        string _log = "[" + playingStatus + "] : video : " + FileName;
        //Debug.Log(_log); //comment out for release
    }

    // -----------------------------------------------------------------------------------------------------
    public void PrePause()
    {
        videoPlayer.Pause();
    }

    // -----------------------------------------------------------------------------------------------------
    public void Rewind()
    {
        videoPlayer.Rewind(false);
        string _log = "[rewind]" + " : video : " + FileName;
        //Debug.Log(_log); //comment out for release
    }

    // -----------------------------------------------------------------------------------------------------
    public void PreRewind()
    {
        videoPlayer.Rewind(false);
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetVideo()
    {
        OpenVideoFile(FileDir, FileName);
        AllocateVideoIntoDisplay();
        videoPlayer.Events.AddListener(OnMediaPlayerEvent);
    }

    // -----------------------------------------------------------------------------------------------------
    void OpenVideoFile(string _fileDir, string _fileName)
    {
        _fileName = ChangeBackDirectorySign(_fileName);
        MediaPlayer mediaPlayerTmp = Instantiate(MediaPlayerPrefab, this.transform) as MediaPlayer;
        //mediaPlayerTmp.MediaPath.Path = System.IO.Path.Combine(_fileDir, _fileName);
        //mediaPlayerTmp.m_VideoPath = _fileName;
        //mediaPlayerTmp.OpenMedia(_location, mediaPlayerTmp.MediaPath.Path, false);
        mediaPlayerTmp.OpenMedia(_location, System.IO.Path.Combine(_fileDir, _fileName), false);
        mediaPlayerTmp.Control.SetLooping(false);
        //Debug.Log("[video] duration : " + mediaPlayerTmp.Info.GetDurationMs()); //comment out for release
        videoPlayer = mediaPlayerTmp;
    }

    // -----------------------------------------------------------------------------------------------------
    string ChangeBackDirectorySign(string _str)
    {
        string _changedStr = _str.Replace("_dirSign_", "/");
        return _changedStr;
    }

    // -----------------------------------------------------------------------------------------------------
    void AllocateVideoIntoDisplay()
    {
        GameObject videoPanelTmp = Instantiate(VideoPanelPrefab, ContentsPanel.transform.Find("Displays")) as GameObject;
        //display = videoPanelTmp.transform.Find("1").GetComponent<DisplayUGUI>();
        display = videoPanelTmp.GetComponent<DisplayUGUI>();
        display.CurrentMediaPlayer = videoPlayer;
        VideoPanel = videoPanelTmp;
    }

    // -----------------------------------------------------------------------------------------------------
    public void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                //Debug.Log("[video] ReadyToPlay : " + FileName); //comment out for release
                break;
            case MediaPlayerEvent.EventType.Started:
                //Debug.Log("[video] Started : " + FileName); //comment out for release
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                canVideoPlay = true;
                //Debug.Log("[video] FirstFrameReady : " + FileName); //comment out for release
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
            case MediaPlayerEvent.EventType.ResolutionChanged:
                //GatherProperties();
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                //Debug.Log("[video] FinishedPlaying : " + FileName); //comment out for release
                break;
        }

        //AddEvent(et);
    }



    // -----------------------------------------------------------------------------------------------------
    public void StartFadingIn()
    {
        fadeState = "fadingIn";
        fadeSpentTimeSec = 0;
    }

    // -----------------------------------------------------------------------------------------------------
    public void StartFadingOut()
    {
        fadeState = "fadingOut";
        fadeSpentTimeSec = 0;
    }

    // -----------------------------------------------------------------------------------------------------
    void DoFading()
    {
        if (fadeState == "fadingIn")
        {
            fadeSpentTimeSec += Time.deltaTime;
            deltaFade = Time.deltaTime / fadeInDurationSec;
            DoFadingIn();
            if (fadeSpentTimeSec >= fadeInDurationSec)
            {
                EndfadingIn();
            }
        }
        else if (fadeState == "fadingOut")
        {
            fadeSpentTimeSec += Time.deltaTime;
            deltaFade = Time.deltaTime / fadeOutDurationSec;
            DoFadingOut();
            if (fadeSpentTimeSec >= fadeOutDurationSec)
            {
                EndfadingOut();
            }
        }
    }

    // -----------------------------------------------------------------------------------------------------
    void DoFadingIn()
    {
        IncreaseVideoAlpha(display);
    }

    // -----------------------------------------------------------------------------------------------------
    void DoFadingOut()
    {
        DecreaseVideoAlpha(display);
    }

    // -----------------------------------------------------------------------------------------------------
    void EndfadingIn()
    {
        fadeState = "none";
    }

    // -----------------------------------------------------------------------------------------------------
    void EndfadingOut()
    {
        Pause();
        fadeState = "none";
    }

    // -----------------------------------------------------------------------------------------------------
    public void CancelPreplaying()
    {
        SetAlphaZero();
        Pause();
    }

    // -----------------------------------------------------------------------------------------------------
    public void CancelFading()
    {
        if (fadeState == "fadingIn")
        {
            SetAlphaZero();
            Pause();
        }
        else if (fadeState == "fadingOut")
        {
            SetAlphaOne();
        }
        fadeState = "none";
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetAlphaZero()
    {
        SetVideoAlphaZero(display);
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetAlphaOne()
    {
        SetVideoAlphaOne(display);
    }

    // -----------------------------------------------------------------------------------------------------
    void SetVideoAlphaOne(DisplayUGUI mediaDisplay)
    {
        Color currentColor = mediaDisplay.color;
        mediaDisplay.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
    }

    // -----------------------------------------------------------------------------------------------------
    void SetVideoAlphaZero(DisplayUGUI mediaDisplay)
    {
        Color currentColor = mediaDisplay.color;
        mediaDisplay.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
    }

    // -----------------------------------------------------------------------------------------------------
    void DecreaseVideoAlpha(DisplayUGUI mediaDisplay)
    {
        Color currentColor = mediaDisplay.color;
        float currentAlpha = currentColor.a;
        float nextAlpha = currentAlpha - deltaFade;
        if (nextAlpha < fadeAlphaMin)
        {
            nextAlpha = fadeAlphaMin;
        }
        mediaDisplay.color = new Color(currentColor.r, currentColor.g, currentColor.b, nextAlpha);
    }

    // -----------------------------------------------------------------------------------------------------
    void IncreaseVideoAlpha(DisplayUGUI mediaDisplay)
    {
        Color currentColor = mediaDisplay.color;
        float currentAlpha = currentColor.a;
        float nextAlpha = currentAlpha + deltaFade;
        if (nextAlpha > fadeAlphaMax)
        {
            nextAlpha = fadeAlphaMax;
        }
        mediaDisplay.color = new Color(currentColor.r, currentColor.g, currentColor.b, nextAlpha);
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetFadeInDurationSec(float _sec)
    {
        fadeInDurationSec = _sec;
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetFadeOutDurationSec(float _sec)
    {
        fadeOutDurationSec = _sec;
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetLoopOn()
    {
        videoPlayer.Control.SetLooping(true);
    }

    // -----------------------------------------------------------------------------------------------------
    public void SetLoopOff()
    {
        videoPlayer.Control.SetLooping(false);
    }



    // -----------------------------------------------------------------------------------------------------
    public int GetVideoWidth()
    {
        return videoPlayer.Info.GetVideoWidth();
    }

    // -----------------------------------------------------------------------------------------------------
    public int GetVideoHeight()
    {
        return videoPlayer.Info.GetVideoHeight();
    }

    // -----------------------------------------------------------------------------------------------------
    public Texture2D ExtractFrame(Texture2D _tex)
    {
        Texture2D _texTmp = videoPlayer.ExtractFrame(_tex);
        return _texTmp;
    }

    // -----------------------------------------------------------------------------------------------------
    public bool GetIsVideoPlaying()
    {
        return videoPlayer.Control.IsPlaying();
    }

    // -----------------------------------------------------------------------------------------------------
    public double GetCurrentVideoTimeSec()
    {
        //return videoPlayer.Control.GetCurrentTime() / 1000.0f;
        return videoPlayer.Control.GetCurrentTime();
    }

}
