using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ChangeScene : MonoBehaviour
{

	DBManager DBManager;

	void Start()
	{	
		if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 5)
			DBManager = GameObject.Find("DBManager").GetComponent<DBManager>();
	}


	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			int currentScene = SceneManager.GetActiveScene().buildIndex;
			Debug.Log("Ho premuto ESC " + currentScene);

			switch(currentScene)
			{
				case 0:
					Application.Quit();
					break;

				case 1:
					changescene(0);
					break;

				case 2:
					changescene(0);
					break;

				case 3:
					changescene(2);
					break;

				case 4:
					TextBack();
					break;

				case 5:
					changescene(3);
					break;

				case 6:
					changescene(0);
					break;
			}
		}
	}

	public void changescene (int indice)
	{
		Change(indice);
	}

	public static void Change(int indice)
	{
		PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene(indice);
	}

	public void onIDChange(int sceneid)
	{
		GameObject textfield = GameObject.Find("InputFieldPOIName");
		string testo = textfield.GetComponent<InputField>().text;

		if(String.IsNullOrEmpty(testo))
		{
			Debug.Log("No POI name inserted!");
			DBManager.VariableText.text = "No POI name inserted!";
		}
		else
		{
			StartCoroutine(WaitforID(testo, sceneid));
		}
	}


	IEnumerator WaitforID(string testo, int sceneid)
	{
		string id = "-1";
		yield return StartCoroutine(DBManager.GetIDFromName(testo));
		id = OpenBrowse.ObtainedID;
		Debug.Log(id);
		if(int.Parse(id) > 0)
		{
			changescene(sceneid);
		}
		else
		{
			Debug.Log("POI not found.");
			DBManager.VariableText.text = "POI not found.";
		}
	}

	public void FotoChange(int indice)
	{
		DeleteManager.table = "FOTO";
		changescene(indice);
	}



	public void VideoChange(int indice)
	{
		DeleteManager.table = "VIDEO";
		changescene(indice);
	}

	public void TextChange(string lingua)
	{


		if(int.Parse(OpenBrowse.ObtainedID) < 0)
		{
			DBManager.VariableText.text = " No POI inserted! You can't add infos!";
		}
		else
		{
			PlayerPrefs.SetString("Lingua", lingua);
			PlayerPrefs.SetInt("TextPreviousScene", SceneManager.GetActiveScene().buildIndex);
			changescene(4);
		}
		
	}

	public static void TextBack()
	{
		int oldIndex = PlayerPrefs.GetInt("TextPreviousScene");
		PlayerPrefs.DeleteKey("TextPreviousScene");
		Change(oldIndex);
	}

}
