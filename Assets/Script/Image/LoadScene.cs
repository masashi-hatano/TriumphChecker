using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class LoadScene : MonoBehaviour
{

    public static string city_name;
    public static int ID;
    public static DateTime time_big;
    public static DateTime time_end;
    public static List<string> paths_photos = null;
    public static bool new_scene = false;
    public Text text;


    private IEnumerator AskUser()
    {
        yield return StartCoroutine(AskForPermissions());
    }

    private IEnumerator AskForPermissions()
    {
        List<bool> permissions = new List<bool>() { false, false };
        List<bool> permissionsAsked = new List<bool>() { false, false };
        List<Action> actions = new List<Action>()
        {
            new Action(() => {
                permissions[0] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
                if (!permissions[0] && !permissionsAsked[0])
                {
                    Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                    permissionsAsked[0] = true;
                    return;
                }
            }),
            new Action(() => {
                permissions[1] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
                if (!permissions[1] && !permissionsAsked[1])
                {
                    Permission.RequestUserPermission(Permission.ExternalStorageRead);
                    permissionsAsked[1] = true;
                    return;
                }
            })
        };
        for (int i = 0; i < permissionsAsked.Count;)
        {
            actions[i].Invoke();
            if (permissions[i])
            {
                ++i;
            }
        }
        yield return new WaitForEndOfFrame();

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadScene.city_name = "paris";
        LoadScene.ID = 0;
        LoadScene.time_big = new DateTime(2021, 11, 16, 00, 00, 00);
        LoadScene.time_end = new DateTime(2021, 11, 16, 23, 59, 59);

        if (!(File.Exists(Application.persistentDataPath
                       + "/MySaveData_" + LoadScene.ID + ".dat")))
        {
            SaveGame(null);
        }

        StartCoroutine(AskUser());

        text.text = "hold";

        LoadScene.ID = 0;

        LoadGame();
        text.text = LoadScene.city_name;
    }

    public static void SaveGame(List<string> paths_photos)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/MySaveData_"+ LoadScene.ID+ ".dat");
        SaveData data = new SaveData();
        data.city_name = city_name;
        data.time_big = time_big;
        data.time_end = time_end;
        data.paths_photos = paths_photos;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/MySaveData_" + LoadScene.ID + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/MySaveData_" + LoadScene.ID + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            LoadScene.city_name = data.city_name;
            LoadScene.time_big = data.time_big;
            LoadScene.time_end = data.time_end;

            if (data.paths_photos == null)
            {
                LoadScene.new_scene = true;
            }
            else
            {
                LoadScene.paths_photos = data.paths_photos;
            }

            Debug.Log("Game data loaded!");
        }
        else
        {
            text.text = "There is no save data!";
        }
    }
}

 [Serializable]
 class SaveData
 {
    public string city_name;
    public DateTime time_big;
    public DateTime time_end;
    public List<string> paths_photos;
 }
