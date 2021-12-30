using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerNew : MonoBehaviour
{

    private Button button;
    private Text text;
    public GameObject panelTextDelete;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        text = GetComponentInChildren<Text>();
        text.text = "New Memory Mode";
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        if (!GameData.NewMemoryMode)
        {
            GameData.NewMemoryMode = true;
            text.text = "Memory Explorer Mode";
            panelTextDelete.SetActive(false);
        }
        else
        {
            GameData.NewMemoryMode = false;
            text.text = "New Memory Mode";
            panelTextDelete.SetActive(true);
        }
    
    }
}