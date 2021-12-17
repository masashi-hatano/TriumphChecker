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
    public Text text_city;


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

        StartCoroutine(AskUser());

        GameData.LoadNode();

        text_city.text = GameData.city_name;
    }
}

