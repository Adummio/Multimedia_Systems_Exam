using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POI;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GalleryDownloader : MonoBehaviour
{
    public static bool downloaded;
    GameObject buffering;

    private void OnEnable()
    {
        buffering = GameObject.Find("Gallery/white");
        downloaded = false;
        StartCoroutine(GetPhoto());
    }

    private void OnDisable()
    {
        foreach (Transform child in TouchManager.container.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator GetPhoto()
    {
        downloaded = false;
        yield return new WaitUntil(() => Web.instantiated);
        yield return new WaitForSeconds(2);
        GameObject container = TouchManager.container;


        GameObject photo = Resources.Load("Photo") as GameObject;
        Poi poi = Web.poiList[ActivateOnTouch.indicePOI];
        GameObject g;
        Texture2D texture;
        Sprite sprite;

        for (int i = 0; i < poi.galleria.Length; i++)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("napolioutdoorams.000webhostapp.com/foto/" + poi.galleria[i]))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    texture = DownloadHandlerTexture.GetContent(uwr);
                    sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    Debug.Log("Ho creato la sprite");

                    g = Instantiate(photo) as GameObject;
                    g.transform.SetParent(container.transform, false);
                    g.GetComponent<Image>().sprite = sprite;
                }
            }
        }
        downloaded = true;
        buffering.SetActive(false);
    }
    
}
