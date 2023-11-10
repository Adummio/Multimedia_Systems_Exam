using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActivateOnTouch : MonoBehaviour
{
    Animator anim;
    GameObject fader;
    GameObject menuParent;
    public static int indicePOI;
    //public bool activateme;
    int prevIndex;
    ScrollSnapRect scrSnap;
    string btnName;



    void Start()
    {
        btnName = "a";
        indicePOI = 0;
        fader = GameObject.Find("CanvasOptions/MainMenuUI/Fader");
        menuParent = GameObject.Find("CanvasOptions/MainMenuUI/MenuParent");
        anim = GameObject.Find("CanvasOptions").GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit))
            {
                btnName = Hit.transform.name;
                indicePOI = System.Convert.ToInt32(btnName);
                
                /*
                if (activateme == true)
                { */

                fader.SetActive(true);
                menuParent.SetActive(true);

                anim.SetTrigger("menu_open");
                TouchManager.menuMoved = true;
            }
        }
    }




}
