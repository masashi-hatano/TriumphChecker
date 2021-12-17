using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/SaveGameData.dat"))
        {
            GameData.LoadGame();
        }
        else
        {
            GameData.player_pos = new Vector3(0,0,0);
            GameData.nodes_pos = new Dictionary<int,Vector3>();
            GameData.nb_nodes = 0;
            GameData.SaveGame();
        }
    }
}
