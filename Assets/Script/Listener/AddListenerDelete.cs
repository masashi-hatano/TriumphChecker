using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerDelete : MonoBehaviour
{

    private Button button;
    private Text text;
    private Text text2;
    public GameObject panelText;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
        text2 = texts[1];
        text.text = "Delete Memory Mode";
        text2.text = "";
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        if (!GameData.DeleteMemoryMode)
        {
            GameData.DeleteMemoryMode = true;
            text.text = "Memory Explorer Mode";
            text2.text = "Touch a memory\nto delete it.";
            panelText.SetActive(false);
        }
        else
        {
            GameData.DeleteMemoryMode = false;
            text.text = "Delete Memory Mode";
            text2.text = "";
            panelText.SetActive(true);
        }

    }
}
