using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : MonoBehaviour
{
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 pivot;

    public RectTransform panelRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        panelRectTransform.anchorMin = new Vector2(0, 0);
        panelRectTransform.anchorMax = new Vector2(1, 1);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
