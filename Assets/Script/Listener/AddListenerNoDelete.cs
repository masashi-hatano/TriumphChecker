using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerNoDelete : MonoBehaviour
{

    private Button button;
    public GameObject fieldObject;
    public GameObject panelTextDelete;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        fieldObject.SetActive(false);
        panelTextDelete.SetActive(true);
        GameData.Creation = false;
    }
}