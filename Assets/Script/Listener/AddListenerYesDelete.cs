using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class AddListenerYesDelete : MonoBehaviour
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
        DeleteNode(GameData.ID);
        fieldObject.SetActive(false);
        panelTextDelete.SetActive(true);
        GameData.Creation = false;
    }

    public void DeleteNode(int ID)
    {
        GameData.nodes_pos.Remove(ID);
        GameData.SaveGame();
        GameData.delete_node.SetActive(false);
    }
}