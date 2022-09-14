using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.GUI;
using UnityEngine;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public Image playDurationImage;
    public Image playPauseImage;
    public Sprite playSprite, pauseSprite;

    private float playedTime = 0;

    public static VideoManager Instance;
    [Space]
    [Header("Game objects")]
    public GameObject loadingObj;

    [Space]
    [Header("Videos")]
    public VideoLink[] videos;
    private VideoLink selectedVideo = null;


    private RectTransform loadingRect;

    private void Awake()
    {
        Instance = this;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        playDurationImage.fillAmount = 0;
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.errorReceived += ErrorVideo;

        loadingRect = loadingObj.GetComponent<RectTransform>();
        playPauseImage.transform.parent.gameObject.SetActive(false);


        selectedVideo = VideoLink.GetRandomVideo(videos);
        videoPlayer.url = selectedVideo.url;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying && videoPlayer.isPrepared)
        {
            if (loadingObj.activeSelf)
            {
                loadingObj.SetActive(false);
                playPauseImage.transform.parent.gameObject.SetActive(true);
            }

            playedTime += Time.deltaTime;
            playDurationImage.fillAmount = playedTime / (float)videoPlayer.length;
        }

        if (loadingObj.activeSelf)
        {
            loadingRect.Rotate(Vector3.back * 80 * Time.deltaTime);
        }

        // if (playedTime >= videoPlayer.length)


    }

    public void PlayPauseVideo()
    {
        if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
            playPauseImage.sprite = pauseSprite;
        }
        else
        {
            videoPlayer.Pause();
            playPauseImage.sprite = playSprite;
        }
    }

    public void Activate(ActionsManager.EndEvent endEvent = null)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        this.endEvent = endEvent;
        AnimationEventManager.Instance.Mute();

        // Select video
        selectedVideo = VideoLink.GetRandomVideo(videos);
        videoPlayer.url = selectedVideo.url;
        videoPlayer.Play();
    }


    public ActionsManager.EndEvent endEvent = null;
    public GameObject videoManagerPrefab;
    public void SetEndEvent(ActionsManager.EndEvent endEvent)
    {
        this.endEvent = endEvent;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vd)
    {
        if (endEvent != null)
            endEvent();
        else
        {
            InitScript.Instance.AddLife(1);
            PlayerPrefs.SetInt("GotExtraLife", 1);
            SweetSugar.Scripts.System.MenuReference.THIS.MenuPlay.SetActive(true);
            SweetSugar.Scripts.System.MenuReference.THIS.LiveShop.gameObject.SetActive(false);
        }

        Instantiate(videoManagerPrefab);
        Destroy(gameObject);
        return;

        ///////////////////////////////
        // Clean video player
        VideoLink.SetWatchedVideo(selectedVideo.id);

        vd.Stop();
        playedTime = 0;
        playDurationImage.fillAmount = 0;
        loadingObj.SetActive(true);
        playPauseImage.transform.parent.gameObject.SetActive(false);
        AnimationEventManager.Instance.Unmute();
        vd.loopPointReached -= EndReached;
        vd.targetTexture.Release();


        if (endEvent != null)
            endEvent();
        else
        {
            InitScript.Instance.AddLife(1);
            PlayerPrefs.SetInt("GotExtraLife", 1);
            SweetSugar.Scripts.System.MenuReference.THIS.MenuPlay.SetActive(true);
            SweetSugar.Scripts.System.MenuReference.THIS.LiveShop.gameObject.SetActive(false);
        }

        SweetSugar.Scripts.MusicBase.Instance.Unmute();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void ErrorVideo(UnityEngine.Video.VideoPlayer source, string message)
    {
        videoPlayer.errorReceived -= ErrorVideo;

        Debug.Log("Catched and fixed");

        // gameObject.SetActive(false);
        // gameObject.SetActive(true);
    }
}

[System.Serializable]
public class VideoLink
{
    public string url;
    public int id;

    private static string playerPrefsKey = "WatchedVideo_";

    public static bool CheckIfWatched(int id)
    {
        int watched = PlayerPrefs.GetInt(playerPrefsKey + id, 0);

        return watched == 1;
    }

    public static void SetWatchedVideo(int id)
    {
        PlayerPrefs.SetInt(playerPrefsKey + id, 1);
        PlayerPrefs.Save();
    }

    public static VideoLink GetRandomVideo(VideoLink[] list)
    {
        List<VideoLink> unwatched = new List<VideoLink>();

        foreach (VideoLink vl in list)
        {
            int watched = PlayerPrefs.GetInt(playerPrefsKey + vl.id, 0);
            if (watched == 0)
                unwatched.Add(vl);
        }

        if (unwatched.Count > 0)
            return unwatched[Random.Range(0, unwatched.Count)];

        return list[Random.Range(0, list.Length)];
    }
}
