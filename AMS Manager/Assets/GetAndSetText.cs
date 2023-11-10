using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GetAndSetText : MonoBehaviour
{
    public InputField InputName;
    public InputField InputLatitude;
    public InputField InputLongitude;
    DBManager DBManager;

    void Start()
    {
        DBManager = GameObject.Find("DBManager").GetComponent<DBManager>();
    }
    
    public void Setget()
    {
        if (String.IsNullOrEmpty(InputName.text) || String.IsNullOrEmpty(InputLatitude.text) || String.IsNullOrEmpty(InputLongitude.text))
        {
            DBManager.VariableText.text = "Not all fields filled!";
        }
        else
        {
            StartCoroutine(Upload());
            
        }
    }

    IEnumerator Upload()
    {
        
        Debug.Log(" \n" + InputName.text);
        
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("nome", InputName.text));
        formData.Add(new MultipartFormDataSection("latitudine", InputLatitude.text));
        formData.Add(new MultipartFormDataSection("longitudine", InputLongitude.text));
        
        using (UnityWebRequest www = UnityWebRequest.Post("napolioutdoorams.000webhostapp.com/InsertPoi.php", formData))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete! New POI ID: " + www.downloadHandler.text);
                OpenBrowse.ObtainedID = www.downloadHandler.text;
                if (int.Parse(www.downloadHandler.text) < 0)
                {
                    DBManager.VariableText.text = "POI not uploaded. Insert correct values.";
                }
                else
                {
                    DBManager.VariableText.text = "POI uploaded with success! Name: " + InputName.text + " , Latitude: " + InputLatitude.text + " , Longitude: " + InputLongitude.text;
                }
                Debug.Log(OpenBrowse.ObtainedID);
            }
        }
        
    }
}
