using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;



public class DeleteManager : MonoBehaviour
{
	public Transform buttonContainer;

	public static string table;

	class Element
	{
		public string id;
		public string nomefile;
	}

	List<Element> listaElementi;

	string jsonstring;

	void Start()
	{
		if(SceneManager.GetActiveScene().buildIndex == 5)
			StartCoroutine(GetList(table));
	}

	public IEnumerator GetList(string tabella)
	{
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add(new MultipartFormDataSection("id", OpenBrowse.ObtainedID));
		formData.Add(new MultipartFormDataSection("tabella", tabella));
        
        using(UnityWebRequest www = UnityWebRequest.Post("napolioutdoorams.000webhostapp.com/GetVideoFoto.php", formData))
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

        listaElementi = JsonConvert.DeserializeObject<List<Element>>(jsonstring);

		GameObject prefab = Resources.Load("Button") as GameObject;

		for(int i = 0; i < listaElementi.Count; i++)
		{

			GameObject bettone = Instantiate(prefab) as GameObject;
	
			bettone.transform.SetParent(buttonContainer);
			var pointerUp = new EventTrigger.Entry();
			pointerUp.eventID = EventTriggerType.PointerUp;
			pointerUp.callback.AddListener((e) => Delete(bettone.name, tabella));
			bettone.name = listaElementi[i].id;
			bettone.GetComponent<EventTrigger>().triggers.Add(pointerUp);
			bettone.GetComponentInChildren<Text>().text = listaElementi[i].nomefile;
		}
	}

	void Delete(string id, string tabella)
	{
		StartCoroutine(DeleteForm(id, tabella));

	}

	IEnumerator DeleteForm(string id, string tabella)
	{
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add(new MultipartFormDataSection("id", id));
		formData.Add(new MultipartFormDataSection("tabella", tabella));
        
        using(UnityWebRequest www = UnityWebRequest.Post("napolioutdoorams.000webhostapp.com/DeleteVideoFoto.php", formData))
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
        			Destroy(GameObject.Find(id));

        	}
        }		
	}
}