using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerNo : MonoBehaviour
{

    private Button button;
    public GameObject fieldObject;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener (OnClickButton);

        fieldObject = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        fieldObject.SetActive(false);
    }
}
