using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameObject TitleBarText;
    /*
    GameObject AudioButton;
    GameObject GalleryButton;
    GameObject VideoButton;
    */
    private int index;

    void Start()
    {
        TitleBarText = GameObject.Find("MenuParent/TitleBar/TitleBarText");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Application.platform == RuntimePlatform.Android)
        {*/
        if (Web.instantiated)
        {
            index = ActivateOnTouch.indicePOI;
            TitleBarText.GetComponent<Text>().text = Web.poiList[index].nome;
        }
        //}
    }

}
