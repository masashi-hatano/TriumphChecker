using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class TransitionToNode : MonoBehaviour
{
    public string LevelToLoad;
    public Text text;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "New Node"))
        {
            CreateNode(new Vector3(0, 0, 0), GameData.nb_nodes.ToString(), new DateTime(2021, 12, 13, 00, 00, 00), new DateTime(2021, 12, 13, 23, 59, 59));
            GameData.ID = GameData.nb_nodes;
            SceneManager.LoadScene(LevelToLoad);
        }

        if (GUI.Button(new Rect(160, 110, 150, 100), "Currrent Node"))
        {
            GameData.ID = GameData.nb_nodes;
            SceneManager.LoadScene(LevelToLoad);
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
}

}
