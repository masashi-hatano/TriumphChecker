using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen_height : MonoBehaviour
{
    private RectTransform panelRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(panelRectTransform.rect.height);
        Debug.Log(panelRectTransform.rect.width);
    }
}
