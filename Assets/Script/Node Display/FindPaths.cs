﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FindPaths
{

    public static List<string> GetAllGalleryImagePaths()
    {

        List<string> results = new List<string>();
        HashSet<string> allowedExtesions = new HashSet<string>() { ".png", ".jpg", ".jpeg" };

        try
        {

            AndroidJavaClass mediaClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media");

            // Set the tags for the data we want about each image.  This should really be done by calling; 
            //string dataTag = mediaClass.GetStatic<string>("DATA");
            // but I couldn't get that to work...

            const string dataTag = "_data";

            string[] projection = new string[] { dataTag };

            AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");

            string[] urisToSearch = new string[] { "EXTERNAL_CONTENT_URI", "INTERNAL_CONTENT_URI" };

            foreach (string uriToSearch in urisToSearch)
            {
                AndroidJavaObject externalUri = mediaClass.GetStatic<AndroidJavaObject>(uriToSearch);
                AndroidJavaObject finder = currentActivity.Call<AndroidJavaObject>("managedQuery", externalUri, projection, null, null, null);
                bool foundOne = finder.Call<bool>("moveToFirst");
                while (foundOne)
                {
                    int dataIndex = finder.Call<int>("getColumnIndex", dataTag);
                    string data = finder.Call<string>("getString", dataIndex);
                    if (allowedExtesions.Contains(Path.GetExtension(data).ToLower()))
                    {
                        string path = @"file://" + data;
                        results.Add(path);
                    }

                    foundOne = finder.Call<bool>("moveToNext");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        return results;
    }
}
