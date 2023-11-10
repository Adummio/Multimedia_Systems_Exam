using Newtonsoft.Json;
using POI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public static string lingua;
    public static bool  instantiated;
    public static List<Poi> poiList;
    
    /*IEnumerator Start()
    {
        Input.location.Start();
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.timestamp);
        }
        StartCoroutine(GetRequest());

    }*/

    private void Start()
    {
        instantiated = false;
        lingua = "ita";     //VA COMMENTATO PERCHE' SI PARTE DALLA LINGUA SETTATA IN SETTINGS

        StartCoroutine(GetRequest());
        //Input.location.Start();
        //Debug.Log(Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
    }

    private void Update()
    {
        //TryInstantiate();
    }

    private void TryInstantiate()
    {
        if (instantiated == false)
        {
            Location actualLocation = new Location(Input.location.lastData.latitude, Input.location.lastData.longitude, 0);
            Debug.Log("Location attuale: " + actualLocation);
            Location centro = new Location(0, 0, 0);

            if (Math.Abs(Location.HorizontalDistance(actualLocation, centro)) == 0)
            {
                Debug.Log("Non è stato possibile geolocalizzare, riprovare. \nLatitudine: " + actualLocation.latitude + "\nLongitudine: " + actualLocation.longitude);
            }
            else
            {
                Debug.Log("Posizione settata alle coordinate: " + actualLocation.latitude + ", " + actualLocation.longitude);
                StartCoroutine(GetRequest());
                instantiated = true;
            }

        }
    }

    public IEnumerator GetRequest()
    {
        //lingua = MenuManager.lingua;          // VA DECOMMENTATO
        string uri = String.Concat("napolioutdoorams.000webhostapp.com/GETAll", String.Concat(lingua, ".php"));
        Debug.Log(uri);
        string jsonString;
        //byte[] video, foto;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for thùe desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
            jsonString = webRequest.downloadHandler.text;
            //jsonString = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
        }

        //Debug.Log("\n" + jsonString + "\n");

        poiList = JsonConvert.DeserializeObject<List<Poi>>(jsonString);

        //Debug.Log("\n" + poiList[0].id + "\n");
        //Debug.Log("\n" + poiList[0].galleria[0] + "\n");

        GameObject prefab = Resources.Load("Capsule") as GameObject;

        for (int i = 0; i < poiList.Count; i++)
        {

            poiList[i].capsula = Instantiate(prefab) as GameObject;
            poiList[i].capsula.name = i.ToString();
            poiList[i].capsula.tag = "Poi";
            //poiList[i].capsula.GetComponent<TextMeshPro>().text = poiList[i].nome;
            //poiList[i].capsula.GetComponent<TextMeshPro>().fontSizeMax = 18;
            poiList[i].capsula.GetComponent<ARLocationPlaceAtLocation>().location = new Location(poiList[i].latitudine, poiList[i].longitudine, 1);


            Debug.Log("Creato " + i);

            /*using (var webRequest = UnityWebRequest0Multimedia.GetAudioClip(poiList[i].audio))
            {
                // Request and wait for the desired page.
                yield return webRequest.Send();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    yield break;
                }
                else
                {
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                }
                    AudioClip audio = DownloadHandlerAudioClip.GetContent(webRequest);
                     
            }*/

            /*using (UnityWebRequest webRequest = UnityWebRequest.Get(poiList[i].video))
            {
                // Request and wait for the desired page.
                yield return webRequest.Send();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                }
                    audio = webRequest.downloadHandler.data;
            }

            for(int j = 0; j < poiList[i].galleria.Length; j++)
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(poiList[i].galleria[j]))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.Send();

                    string[] pages = uri.Split('/');
                    int page = pages.Length - 1;

                    if (webRequest.isNetworkError)
                    {
                        Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    }
                    else
                    {
                        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    }
                        foto = webRequest.downloadHandler.data;
                         
                }
            }*/

        }

        instantiated = true;
    }
   
    private void OnDisable()
    {
        print("Disabilitato");
        DeleteAll();
        StopCoroutine(GetRequest());
        Debug.Log("STO disabllll ");
        instantiated = false;
        Input.location.Stop();
    }

    private void OnDestroy()
    {
        instantiated = false;
        Debug.Log("STO distruggendo ");

    }

    private void DeleteAll()
    {
        int count = 0;
        Debug.Log("strunz " + count);
        foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
        {
            if (o.tag == "Poi")
            {
                Destroy(o);
                count++;
            }

            Debug.Log("Count " + count);
        }
    }

}