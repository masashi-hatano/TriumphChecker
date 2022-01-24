using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class AddListenerEnter : MonoBehaviour
{

    private Button button;
    public GameObject fieldObject;
    public GameObject panelText;
    public Text inputFieldCity;
    public Text inputFieldDateBig;
    public Text inputFieldDateEnd;
    public Text text;
    public GameObject memory;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        DateTime time_big;
        DateTime time_end;

        try
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            time_big = DateTime.ParseExact(inputFieldDateBig.text + " 00:00:00", format, provider);
            time_end = DateTime.ParseExact(inputFieldDateEnd.text + " 23:59:59", format, provider);
            if (time_end < time_big)
            {
                text.text = "Wrong order of date";
            }
            else 
            {
                CreateNode(GameData.click_pos, inputFieldCity.text, time_big, time_end);
                fieldObject.SetActive(false);
                panelText.SetActive(true);
                GameData.Creation = false;
            }
        }
        catch (System.Exception e)
        {
            text.text = "Wrong format, try again";
        }
    }

    public void CreateNode(Vector3 pos, string city_name, DateTime time_big, DateTime time_end)
    {
        GameData.nb_nodes++;
        GameData.nodes_pos.Add(GameData.nb_nodes, pos);
        GameData.SaveGame();
        GameData.ID = GameData.nb_nodes;
        GameData.city_name = city_name;
        GameData.time_big = time_big;
        GameData.time_end = time_end;
        GameData.SaveNode(null);
        GameObject memo = Instantiate(memory, pos, Quaternion.identity);
        foreach (Transform child in memo.transform)
        {
            child.GetComponent<TextMesh>().text = city_name + "\n\n\n\n\n\n\n\n\n\n\n" + time_big.ToString("d", CultureInfo.CreateSpecificCulture("en-US")) + " - " + time_end.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
        }
        ShowVerificationText script1 = memo.GetComponent<ShowVerificationText>();
        ShowDeleteText script2 = memo.GetComponent<ShowDeleteText>();
        script1.ID = GameData.nb_nodes;
        script2.ID = GameData.nb_nodes;
    }
}