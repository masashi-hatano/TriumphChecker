using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerNew : MonoBehaviour
{

    private Button button;
    private Text text;
    private Text text2;
    public GameObject panelTextDelete;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
        text2 = texts[1];
        text.text = "New Memory Mode";
        text2.text = "";
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        if (!GameData.NewMemoryMode)
        {
            GameData.NewMemoryMode = true;
            text.text = "Memory Explorer Mode";
            text2.text = "Touch the ground\nto create a new memory.";
            panelTextDelete.SetActive(false);
        }
        else
        {
            GameData.NewMemoryMode = false;
            text.text = "New Memory Mode";
            text2.text = "";
            panelTextDelete.SetActive(true);
        }
    
    }
}