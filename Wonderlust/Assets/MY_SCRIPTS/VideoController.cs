using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(VideoRoutine());
    }

    private IEnumerator VideoRoutine()
    {
        yield return new WaitUntil(() => Web.instantiated);
        gameObject.GetComponent<VideoPlayer>().url = System.String.Concat("http://napolioutdoorams.000webhostapp.com/video/", Web.poiList[ActivateOnTouch.indicePOI].videos[0]);
    }


}
