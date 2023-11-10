using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;


public class DBManager : MonoBehaviour
{

	GetAndSetText gettone;
	public Text VariableText;
	string result;

	void Start()
	{	
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			gettone = GameObject.Find("TextManager").GetComponent<GetAndSetText>();
		}
		
		result = "-1";
	}


	static public IEnumerator GetIDFromName(string nomepoi)
	{
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

		formData.Add(new MultipartFormDataSection("nomepoi", nomepoi));

		using (UnityWebRequest www = UnityWebRequest.Post("napolioutdoorams.000webhostapp.com/PoiIDfromName.php", formData))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!" + www.downloadHandler.text);
				OpenBrowse.ObtainedID = www.downloadHandler.text;
			}
		}
	}

	IEnumerator PostForm(string modifica, string phpurl)
	{

		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

		formData.Add(new MultipartFormDataSection("id", OpenBrowse.ObtainedID));
		formData.Add(new MultipartFormDataSection("modifica", modifica));


		using (UnityWebRequest www = UnityWebRequest.Post(phpurl, formData))
		{
			yield return www.SendWebRequest();


			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!" + www.downloadHandler.text);
				result = www.downloadHandler.text;
			}
		}
	}


	IEnumerator VideoInsert(string video)
	{
		yield return StartCoroutine(PostForm(video, "napolioutdoorams.000webhostapp.com/InsertVideo.php"));
		if(result == "0")
		{
			VariableText.text = "Video inserted with success!";		
		}
        else
        {
            VariableText.text = "Video not inserted.";
        }
		result = "-1";
	}

	IEnumerator FotoInsert(string foto)
	{
		yield return StartCoroutine(PostForm(foto, "napolioutdoorams.000webhostapp.com/InsertPhoto.php"));
		if(result == "0")
		{
			VariableText.text = "Photo inserted with success!";
		}
        else
        {
            VariableText.text = "Photo not inserted.";
        }
		result = "-1";
	}

	IEnumerator AudioITAInsert(string audio)
	{
		yield return StartCoroutine(PostForm(audio, "napolioutdoorams.000webhostapp.com/UpdateAudioITA.php"));
		if(result == "0")
		{
			VariableText.text = "Audio [LANGUAGE : ITA] inserted with success!";
		}
        else
        {
            VariableText.text = "Audio [LANGUAGE : ITA] not inserted.";
        }
		result = "-1";
	}

	IEnumerator AudioENGInsert(string audio)
	{
		yield return StartCoroutine(PostForm(audio, "napolioutdoorams.000webhostapp.com/UpdateAudioENG.php"));
		if(result == "0")
		{	
			VariableText.text = "Audio [LANGUAGE : ENG] inserted with success!";
		}
        else
        {
            VariableText.text = "Audio [LANGUAGE : ENG] not inserted.";
        }
		result = "-1";
	}

	IEnumerator AudioFRAInsert(string audio)
	{
		yield return StartCoroutine(PostForm(audio, "napolioutdoorams.000webhostapp.com/UpdateAudioFRA.php"));
		if(result == "0")
		{
			VariableText.text = "Audio [LANGUAGE : FRA] inserted with success!";		
		}
        else
        {
            VariableText.text = "Audio [LANGUAGE : FRA] not inserted.";
        }
		result = "-1";
	}

	public void InserisciVideo(string video)
	{
		StartCoroutine(VideoInsert(video));
	}

	public void InserisciFoto(string foto)
	{
		StartCoroutine(FotoInsert(foto));
		
	}

	IEnumerator NewName(string testo)
	{
		yield return StartCoroutine(PostForm(testo, "napolioutdoorams.000webhostapp.com/UpdateName.php" ));
		if(result == "0")
		{
			VariableText.text = "Name modified with success!";
		}
        else
        {
            VariableText.text = "Name not valid.";
        }
		result = "-1";
	}

	IEnumerator NewLatitude(string testo)
	{
		yield return StartCoroutine(PostForm(testo, "napolioutdoorams.000webhostapp.com/UpdateLatitude.php" ));
		if(result == "0")
		{
			VariableText.text = "Latitude modified with success!";
		}
        else
        {
            VariableText.text = "Latitude not valid.";
        }
		result = "-1";
	}

	IEnumerator NewLongitude(string testo)
	{
		yield return StartCoroutine(PostForm(testo, "napolioutdoorams.000webhostapp.com/UpdateLongitude.php"));
		if(result == "0")
		{
			VariableText.text = "POI longitude modified with success!";
		}
        else
        {
            VariableText.text = "Longitude not valid.";
        }
		result = "-1";
	}

	public void ModificaNome()
	{
		GameObject textfield = GameObject.Find("NAME");
		string testo = textfield.GetComponent<InputField>().text;
		StartCoroutine(NewName(testo));		
	}

	public void ModificaLatitudine()
	{
		GameObject textfield = GameObject.Find("LATITUDE");
		string testo = textfield.GetComponent<InputField>().text;
		StartCoroutine(NewLatitude(testo));
		
	}

	public void ModificaLongitude()
	{
		GameObject textfield = GameObject.Find("LONGITUDE");
		string testo = textfield.GetComponent<InputField>().text;	
		StartCoroutine(NewLongitude(testo));
		
	}


	IEnumerator TestInfoITA(string testo)
	{
		yield return StartCoroutine(PostForm(testo, "napolioutdoorams.000webhostapp.com/UpdateInfoITA.php"));
		if(result == "0")
		{
			VariableText.text = "Info [LANGUAGE : ITA] inserted with success!";
			yield return new WaitForSeconds(1.5f);
			ChangeScene.TextBack();
		}
        else
        {
            VariableText.text = "Info [LANGUAGE : ITA] not valid.";
        }
	}

	IEnumerator TestInfoENG(string testo)
	{
		yield return StartCoroutine(PostForm(testo, "napolioutdoorams.000webhostapp.com/UpdateInfoENG.php"));
		if(result == "0")
		{
			VariableText.text = "Info [LANGUAGE : ENG] inserted with success!";
			yield return new WaitForSeconds(1.5f);
			ChangeScene.TextBack();
		}
        else
        {
            VariableText.text = "Info [LANGUAGE : ENG] not inserted.";
        }
	}

	IEnumerator TestInfoFRA(string testo)
	{
		yield return StartCoroutine(PostForm(testo,"napolioutdoorams.000webhostapp.com/UpdateInfoFRA.php" ));
		if(result == "0")
		{
			VariableText.text = "Info [LANGUAGE : FRA] inserted with success!";
			yield return new WaitForSeconds(1.5f);
			ChangeScene.TextBack();
		}
        else
        {
            VariableText.text = "Info [LANGUAGE : FRA] not inserted.";
        }
	}


	public void ModificaInfoITA()
	{
		GameObject textfield = GameObject.Find("TextMeshPro - InputField");
		string testo = textfield.GetComponent<TMP_InputField>().text;
		StartCoroutine(TestInfoITA(testo));
	}

	public void ModificaInfoENG()
	{
		GameObject textfield = GameObject.Find("TextMeshPro - InputField");
		string testo = textfield.GetComponent<TMP_InputField>().text;
		StartCoroutine(TestInfoENG(testo));
		
	}

	public void ModificaInfoFRA()
	{
		GameObject textfield = GameObject.Find("TextMeshPro - InputField");
		string testo = textfield.GetComponent<TMP_InputField>().text;
		StartCoroutine(TestInfoFRA(testo));
		
	}

	public void ModificaInfo()
	{
		if(PlayerPrefs.GetString("Lingua") == "ITALIANO")
		{
			ModificaInfoITA();
		}
		else if(PlayerPrefs.GetString("Lingua") == "INGLESE")
		{
			ModificaInfoENG();
		}
		else
		{
			ModificaInfoFRA();
		}

	}


	public void ModificaAudioITA(string audio)
	{
		StartCoroutine(AudioITAInsert(audio));
		
	}

	public void ModificaAudioENG(string audio)
	{
		StartCoroutine(AudioENGInsert(audio));
	}

	public void ModificaAudioFRA(string audio)
	{
		StartCoroutine(AudioFRAInsert(audio));
	}

}