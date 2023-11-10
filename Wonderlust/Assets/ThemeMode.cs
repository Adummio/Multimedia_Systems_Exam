using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThemeMode : MonoBehaviour
{
    static Sprite bright;
    static Sprite dark;
    static GameObject wallpaper;
    Animator anim;

    private void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        wallpaper = GameObject.Find("ImageDay");
        dark = Resources.Load<Sprite>("Dark");
        bright = Resources.Load<Sprite>("Bright");
        //Debug.Log(System.DateTime.Now);
    }

    public void DarkMode()
    {
        if (wallpaper.GetComponent<Image>().sprite == bright)
        {
            StartCoroutine(GoDark());
        }
    }

    IEnumerator GoDark()
    {
        anim.SetTrigger("DarkTheme");
        yield return new WaitForSeconds(1.5f);
        wallpaper.GetComponent<Image>().sprite = dark;
    }

    public void BrightMode()
    {
        if(wallpaper.GetComponent<Image>().sprite == dark)
        {
            StartCoroutine(GoBright());
        }
    }

    IEnumerator GoBright()
    {
        anim.SetTrigger("BrightTheme");
        yield return new WaitForSeconds(0.5f);
        wallpaper.GetComponent<Image>().sprite = bright;
    }

}
