using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class LoadGame : MonoBehaviour
{
    public GameObject player;
    public GameObject memory;
    public GameObject panel;
    public GameObject panelText;
    public GameObject panelCreate;
    public GameObject panelTextDelete;
    public GameObject panelDelete;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (File.Exists(Application.persistentDataPath
                       + "/SaveGameData.dat"))
        {
            GameData.LoadGame();
        }
        else
        {
        GameData.player_pos = new Vector3(277,1,206);
        GameData.nodes_pos = new Dictionary<int,Vector3>();
        GameData.nb_nodes = 0;
        GameData.SaveGame();
        }

        player.transform.position = GameData.player_pos;
        GameData.panel = GameObject.Find("Panel");
        GameData.panelText = GameObject.Find("PanelText");
        GameData.panelCreate = GameObject.Find("PanelCreate");
        GameData.panelTextDelete = GameObject.Find("PanelTextDelete");
        GameData.panelDelete = GameObject.Find("PanelDelete");
        GameData.text = text;

        GameData.panel.SetActive(false);
        GameData.panelCreate.SetActive(false);
        GameData.panelDelete.SetActive(false);

        GameData.NewMemoryMode = false;
        GameData.DeleteMemoryMode = false;
        GameData.Creation = false;

        foreach (KeyValuePair<int,Vector3> memory_pos in GameData.nodes_pos)
        {
            GameObject memo = Instantiate(memory, memory_pos.Value, Quaternion.identity);
            GameData.ID = memory_pos.Key;
            GameData.LoadNode();
            foreach (Transform child in memo.transform)
            {
                child.GetComponent<TextMesh>().text = GameData.city_name + "\n\n\n\n\n\n\n\n\n\n\n" + GameData.time_big.ToString("d", CultureInfo.CreateSpecificCulture("en-US")) + " - " + GameData.time_end.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
            }
            ShowVerificationText script1 = memo.GetComponent<ShowVerificationText>();
            ShowDeleteText script2 = memo.GetComponent<ShowDeleteText>();
            script1.ID = memory_pos.Key;
            script2.ID = memory_pos.Key;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
