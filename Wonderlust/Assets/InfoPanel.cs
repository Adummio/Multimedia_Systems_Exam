using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(InfoRoutine());
    }

    private IEnumerator InfoRoutine()
    {
        yield return new WaitUntil(() => Web.instantiated);
        gameObject.GetComponent<Text>().text = Web.poiList[ActivateOnTouch.indicePOI].info;

    }
}
