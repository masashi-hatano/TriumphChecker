using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameData
{
    public static GameObject panel;
    public static GameObject panelText;
    public static GameObject panelCreate;
    public static GameObject panelTextDelete;
    public static GameObject panelDelete;
    public static Text text;

    public static Vector3 player_pos;
    public static Dictionary<int,Vector3> nodes_pos;
    public static int nb_nodes;

    public static bool NewMemoryMode;
    public static bool DeleteMemoryMode;
    public static bool Creation;

    public static Vector3 click_pos;
    public static GameObject delete_node;


    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/SaveGameData.dat");
        SaveGameData data = new SaveGameData();
        data.player_pos = new List<float>();
        data.player_pos.Add(player_pos.x);
        data.player_pos.Add(player_pos.y);
        data.player_pos.Add(player_pos.z);

        data.nodes_ID = new List<int>();
        data.nodes_x = new List<float>();
        data.nodes_y = new List<float>();
        data.nodes_z = new List<float>();
        foreach (KeyValuePair<int, Vector3> entry in nodes_pos)
        {
            data.nodes_ID.Add(entry.Key);
            data.nodes_x.Add(entry.Value.x);
            data.nodes_y.Add(entry.Value.y);
            data.nodes_z.Add(entry.Value.z);
        }
        
        data.nb_nodes = nb_nodes;
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/SaveGameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/SaveGameData.dat", FileMode.Open);
            SaveGameData data = (SaveGameData)bf.Deserialize(file);
            file.Close();

            GameData.player_pos = new Vector3(data.player_pos[0], data.player_pos[1], data.player_pos[2]);

            GameData.nodes_pos = new Dictionary<int, Vector3>();
            for (int i=0; i < data.nodes_ID.Count; i++)
            {
                Vector3 pos = new Vector3(data.nodes_x[i], data.nodes_y[i], data.nodes_z[i]);
                GameData.nodes_pos.Add(data.nodes_ID[i], pos);
            }
            
            GameData.nb_nodes = data.nb_nodes;

            Debug.Log("Game data loaded!");
        }
    }


    public static string city_name;
    public static int ID;
    public static DateTime time_big;
    public static DateTime time_end;
    public static List<string> paths_photos;
    public static bool new_scene;

    public static void SaveNode(List<string> paths_photos)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/SaveNodeData_" + GameData.ID + ".dat");
        SaveNodeData data = new SaveNodeData();
        data.city_name = city_name;
        data.time_big = time_big;
        data.time_end = time_end;
        data.paths_photos = paths_photos;
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadNode()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/SaveNodeData_" + GameData.ID + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/SaveNodeData_" + GameData.ID + ".dat", FileMode.Open);
            SaveNodeData data = (SaveNodeData)bf.Deserialize(file);
            file.Close();

            GameData.city_name = data.city_name;
            GameData.time_big = data.time_big;
            GameData.time_end = data.time_end;

            if (data.paths_photos == null)
            {
                GameData.new_scene = true;
                GameData.paths_photos = null;
            }
            else
            {
                GameData.new_scene = false;
                GameData.paths_photos = data.paths_photos;
            }

            Debug.Log("Game data loaded!");
        }
    }


}

[Serializable]
class SaveNodeData
{
    public string city_name;
    public DateTime time_big;
    public DateTime time_end;
    public List<string> paths_photos;
}

[Serializable]
class SaveGameData
{
    public List<float> player_pos;
    public List<int> nodes_ID;
    public List<float> nodes_x;
    public List<float> nodes_y;
    public List<float> nodes_z;
    public int nb_nodes;
}
