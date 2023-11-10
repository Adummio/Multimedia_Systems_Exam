using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class AudioDownloader : MonoBehaviour
{

    public static AudioClip clip;

    void Start ()
    {
       DownloadAudio();
    }


    void DownloadAudio()
    {
        StartCoroutine(GetAudioClip());
    }
//Web.poiList[ActivateOnTouch.indicePOI].audio
    IEnumerator GetAudioClip() 
    {
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("napolioutdoorams.000webhostapp.com/audio/" + Web.poiList[ActivateOnTouch.indicePOI].audio, AudioType.OGGVORBIS))
        {
            //Debug.Log("CIAO");
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log("ERR");
                Debug.LogError(uwr.error);
                yield break;
            }
            //Debug.Log("NO_ERR");
            clip = DownloadHandlerAudioClip.GetContent(uwr);
            AudioSource audio = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
            audio.clip = clip;
            //audio.playOnAwake = true;
            
        }
    }  
}