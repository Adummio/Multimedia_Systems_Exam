using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

public class DeletePoi : MonoBehaviour
{
	public Transform buttonContainer;

	class Poi
	{
		public string id;
		public string nome;
	}

	List<Poi> listaPoi;
	string jsonstring;

	void Start()
	{
		StartCoroutine(GetCreate());
	}

	IEnumerator GetCreate()
	{        
        using(UnityWebRequest www = UnityWebRequest.Get("napolioutdoorams.000webhostapp.com/GetPoi.php"))
        {
        	yield return www.SendWebRequest();

        	if(www.isNetworkError || www.isHttpError)
        	{
        		Debug.Log(www.error);
        	}
        	else
        	{
        		Debug.Log(www.downloadHandler.text);
        		jsonstring = www.downloadHandler.text;
        	}
        }

        listaPoi = JsonConvert.DeserializeObject<List<Poi>>(jsonstring);

		GameObject prefab = Resources.Load("Button") as GameObject;

		for(int i = 0; i < listaPoi.Count; i++)
		{

			GameObject bottone = Instantiate(prefab) as GameObject;
	
			bottone.transform.SetParent(buttonContainer);
			var pointerUp = new EventTrigger.Entry();
			pointerUp.eventID = EventTriggerType.PointerUp;
			pointerUp.callback.AddListener((e) => Delete(bottone.name));
			bottone.name = listaPoi[i].id;
			bottone.GetComponent<EventTrigger>().triggers.Add(pointerUp);
			bottone.GetComponentInChildren<Text>().text = listaPoi[i].nome;
		}
	}

	void Delete(string id)
	{
		StartCoroutine(DeleteForm(id));
	}

	IEnumerator DeleteForm(string id)
	{
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add(new MultipartFormDataSection("id", id));

		using(UnityWebRequest www = UnityWebRequest.Post("napolioutdoorams.000webhostapp.com/DeletePoi.php", formData))
		{
			yield return www.SendWebRequest();

			if(www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log(www.downloadHandler.text);

				if(www.downloadHandler.text == "0")
				{
					Destroy(GameObject.Find(id));
				}
			}
		}
	}


}