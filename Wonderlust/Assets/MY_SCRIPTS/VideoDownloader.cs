using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System.Collections;

public class VideoDownloader : MonoBehaviour
{

	void OnEnable()
	{
		StartCoroutine(CreateButtons());
	}

    private IEnumerator CreateButtons()
	{
        yield return new WaitUntil(() => Web.instantiated);
        GameObject prefab = Resources.Load("Button") as GameObject;
		int arrindex = ActivateOnTouch.indicePOI;

		for(int i = 0; i < Web.poiList[arrindex].videos.Length; i++)
		{

			GameObject bettone = Instantiate(prefab) as GameObject;
	
			bettone.transform.SetParent(gameObject.transform);
			var pointerUp = new EventTrigger.Entry();
			pointerUp.eventID = EventTriggerType.PointerUp;
			pointerUp.callback.AddListener((e) => Ontouch(bettone.name));
			bettone.name = Web.poiList[arrindex].videos[i];
			bettone.tag = "Video";
			bettone.GetComponent<EventTrigger>().triggers.Add(pointerUp);
			bettone.GetComponentInChildren<Text>().text = Web.poiList[arrindex].videos[i];
		}
	}
	void Ontouch(string video)
	{
        TouchManager.videoPlayer.SetActive(true);
		TouchManager.videoPlayer.GetComponent<VideoPlayer>().url = "http://napolioutdoorams.000webhostapp.com/video/" + video;
	}

	void OnDisable()
	{
		foreach(GameObject o in GameObject.FindObjectsOfType<GameObject>())
		{
			if(o.tag == "Video")
				Destroy(o);
		}
	}
}


