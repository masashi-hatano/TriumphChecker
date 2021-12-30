using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddListenerDelete : MonoBehaviour
{

    private Button button;
    private Text text;
    public GameObject panelText;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        text = GetComponentInChildren<Text>();
        text.text = "Delete Memory Mode";
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        if (!GameData.DeleteMemoryMode)
        {
            GameData.DeleteMemoryMode = true;
            text.text = "Memory Explorer Mode";
            panelText.SetActive(false);
        }
        else
        {
            GameData.DeleteMemoryMode = false;
            text.text = "Delete Memory Mode";
            panelText.SetActive(true);
        }

    }
}
