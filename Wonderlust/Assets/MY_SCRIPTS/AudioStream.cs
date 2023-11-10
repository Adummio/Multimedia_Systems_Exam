using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStream : MonoBehaviour
{
    public string url;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AudioPlayer());
        }
    }

    private IEnumerator AudioPlayer()
    {
        WWW music = new WWW(url);
        AudioClip audio = music.GetAudioClip(false, true, AudioType.WAV);

        if(audio==null || audio.loadState == AudioDataLoadState.Unloaded)
        {
            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<AudioSource>().clip = audio;
        GetComponent<AudioSource>().Play();
    }
}
