using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class GlimpseController : MonoBehaviour
{
    public VideoClip[] VideoClips;

    private VideoPlayer mVideoPlayer;
    private AudioSource mAudioSource;
    private int mNextClipIndex;

    private void Start()
    {
        mNextClipIndex = 0;
        mVideoPlayer = GetComponent<VideoPlayer>();
        mAudioSource = GetComponent<AudioSource>();

        //Assign the Audio from Video to AudioSource to be played
        mVideoPlayer.EnableAudioTrack(0, true);
        mVideoPlayer.SetTargetAudioSource(0, mAudioSource);
    }

    public void Activate()
    {
        if (VideoClips.Length > mNextClipIndex)
        {
            mVideoPlayer.clip = VideoClips[mNextClipIndex];
            mVideoPlayer.Prepare();
            mVideoPlayer.Play();
            mAudioSource.Play();
        }

        mNextClipIndex = (mNextClipIndex + 1) % VideoClips.Length;
    }
}
