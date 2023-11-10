using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    public int previousScene;
    static int currentScene;

    GameObject credits;
    GameObject exit;
    GameObject settings;
    public static bool creditsMoved;
    public static bool settingsMoved;


    GameObject audioPlayer;
    GameObject menuParent;
    GameObject fader;
    GameObject back;
    GameObject backGallery;
    GameObject infoPanel;
    GameObject canvasOptions;
    public static GameObject videoPlayer;
    public static GameObject gallery;
    public static GameObject scrollSnap;
    public static GameObject infoText;
    public static GameObject container;
    public static bool menuMoved;

    Animator anim;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        {
            Screen.orientation = ScreenOrientation.Portrait;

            anim = GameObject.Find("Canvas").GetComponent<Animator>();
            settings = GameObject.Find("Settings");
            credits = GameObject.Find("Credits");
            fader = GameObject.Find("Canvas/Fader");
            exit = GameObject.Find("Canvas/ExitButton");
            credits.transform.hasChanged = false;
            settings.transform.hasChanged = false;
            creditsMoved = false;
            settingsMoved = false;
            /*
            credits.SetActive(true);
            fader.SetActive(true);
            exit.SetActive(true);
            */
        }
        else if (currentScene == 1)
        {
            Screen.orientation = ScreenOrientation.Landscape;

            audioPlayer = GameObject.Find("CanvasOptions/MainMenuUI/AudioPlayer");
            menuParent = GameObject.Find("MenuParent");
            fader = GameObject.Find("Fader");
            back = GameObject.Find("CanvasOptions/MainMenuUI/MenuParent/TitleBar/BackButton");
            backGallery = GameObject.Find("Gallery/BackButton");
            videoPlayer = GameObject.Find("VideoPlayer");
            infoPanel = GameObject.Find("CanvasOptions/MainMenuUI/InfoPanel");
            canvasOptions = GameObject.Find("CanvasOptions");
            gallery = GameObject.Find("Gallery");
            container = GameObject.Find("Gallery/Panel/ScrollSnap/Container");
            scrollSnap = GameObject.Find("ScrollSnap");
            infoText = GameObject.Find("InfoPanel");

            videoPlayer.SetActive(false);
            fader.SetActive(true);
            menuParent.SetActive(true);
            audioPlayer.SetActive(false);
            infoPanel.SetActive(false);
            gallery.SetActive(false);
            infoText.SetActive(false);
            menuMoved = false;
        }

        else if (currentScene == 2)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (Application.platform == RuntimePlatform.Android)
        {*/
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Ho premuto ESC");

            if (currentScene == 0)
            {
                //Debug.Log("CREDITS" + credits.transform.position);

                Debug.Log("CREDITS" + credits.transform.hasChanged);
                Debug.Log("SETTINGS" + settings.transform.hasChanged);
                if (creditsMoved)//(360.0f, 640.0f, 0.0f))
                {
                    anim.SetTrigger("CreditsCloser");
                    Debug.Log("CREDITS" + credits.transform.hasChanged);
                    creditsMoved = false;
                }


                else if (settingsMoved)//position == new Vector3(360.0f, 640.0f, 0.0f))
                {
                    anim.SetTrigger("SettingsCloser");
                    settingsMoved = false;
                }

                else
                {
                    Application.Quit();
                }
            }
            
            else if (currentScene == 1)
            {
                anim = GameObject.Find("CanvasOptions").GetComponent<Animator>();
                //Debug.Log("Sono nella scena 1");
                Debug.Log("MENUPARENT" + menuMoved);

                if (menuMoved)
                {
                    Debug.Log("Il menuParent è attivo");
                    
                    if (audioPlayer.activeInHierarchy)//transform.position != new Vector3(640f, 109.2f, 0.0f))
                    {
                        Debug.Log("L'audioPlayer è attivo");
                        Debug.Log(audioPlayer.transform.position);
                        back.SetActive(false);
                        audioPlayer.SetActive(false);
                    }

                    else if (gallery.activeInHierarchy)
                    {
                        Debug.Log("GALLERIA" + gallery.transform.position);
                        backGallery.SetActive(false);
                        gallery.SetActive(false);
                    }

                    else if (infoPanel.activeInHierarchy)//transform.position != new Vector3(640f, 109.2f, 0.0f))
                    {
                        Debug.Log("L'infoPanel è attivo");
                        Debug.Log(infoPanel.transform.position);
                        back.SetActive(false);
                        infoPanel.SetActive(false);
                    }

                    else
                    {
                        Debug.Log("L'audioPlayer è disattivo");
                        anim.SetTrigger("menu_close");
                        menuMoved = false;
                        /*
                        menuParent.SetActive(false);
                        fader.SetActive(false);
                        */
                    }

                    return;
                }
                
                else if (videoPlayer.activeInHierarchy)
                {
                    Debug.Log("Il videoPlayer è attivo");
                    videoPlayer.SetActive(false);
                    canvasOptions.SetActive(true);
                }
                
                      
                else
                {
                    Debug.Log("Voglio tornare alla scena precedente");
                    SceneManager.LoadScene(previousScene);
                }
                  
                 return;
            }

            else
            {
                SceneManager.LoadScene(previousScene);
            }

            return;
        }
        //}
    }


    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void SceneLoaderAdditive(int SceneIndex)
    {
        SceneManager.LoadSceneAsync(SceneIndex);
    }

    public void creditsMove()
    {
        creditsMoved = true;
    }

    public void settingsMove()
    {
        settingsMoved = true;
    }
}


