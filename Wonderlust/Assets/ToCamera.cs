using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCamera : MonoBehaviour
{
    Animator anim;
    /*
    public void Camera()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        StartCoroutine(GoCamera());
    }

    IEnumerator GoCamera()
    {
        anim.SetTrigger("ToCamera");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }*/
}
