using System.Collections;
using UnityEngine;
using UnityEngine.Video;


//Authors Matt Smith & Shaun Ferns

[RequireComponent(typeof(VideoPlayer))] //this calls the basic video instructions for handling video
[RequireComponent(typeof(AudioSource))] //this calls the basic instruction for handling audio

public class PlayVideo : MonoBehaviour
{
    public VideoClip videoClip; //create a public var of videoClip to handle video stream
    public VideoClip videoClip2;

    private VideoPlayer videoPlayer; //create a private Video Player Var that instanciates video player
    private AudioSource audioSource; //create a private AudioSource var that instanciates audio stream

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>(); //Upon start get the video Player component
        audioSource = GetComponent<AudioSource>(); //Upon start get the audio player component

        videoPlayer.playOnAwake = true;
        audioSource.playOnAwake = false;

        // assign video clip
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        // setup AudioSource 
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // render video to main texture of assigned GameObject
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
    }
    private void Update() 
    {
        PlayVideo2();
    }
    void PlayVideo2()
    {
        StartCoroutine("PlayNext");
    }
    IEnumerator PlayNext()
    {
        yield return new WaitForSeconds(8);
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip2;
    }
}