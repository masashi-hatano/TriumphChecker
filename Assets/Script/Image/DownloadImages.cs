using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using System;
using System.Linq;

public class DownloadImages : DownloadExif
{
    public RawImage imageHolder;
    private List<Texture2D> textures = new List<Texture2D>();
    private DateTime time1 = new DateTime(2021,11,15,00,00,00);
    private DateTime time2 = new DateTime(2021,11,16,23,59,59);
    public float changeTime = 10.0f;
    private int currentSlide = 0;
    private float timeSinceLast = 1.0f;
    private List<string> galleryImages;
    

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
            text.text = (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) && Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)).ToString();
            if (permissions[i])
            {
                ++i;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator LoadImages()
    {
        System.Random rnd = new System.Random();

        foreach (string path in this.galleryImages)
        {
            this.imagePath = path;
            yield return StartCoroutine(LoadTexture());
            if (this.texture != null)
            {
                k++;
                text.text = k.ToString();
                this.textures.Add(this.texture);
            }
        }

        if (this.textures.Count > 10)
        {
            this.textures = this.textures.OrderBy(x => rnd.Next()).Take(10).ToList();
        }
        if (this.textures.Count > 0)
        {
            imageHolder.texture = this.textures[0];
            imageHolder.SizeToTexture();
            currentSlide = (currentSlide + 1) % this.textures.Count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(AskForPermissions());

        this.time_big = time1;
        this.time_end = time2;

        //galleryImages = new List<string>();
        galleryImages = FindPaths.GetAllGalleryImagePaths();

        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151650.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151651.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151652.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151654.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151654_1.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151656.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151643.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151645.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151647.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151648.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/IMG_20211116_151649.jpg");
        //galleryImages.Add("file:///C:/Users/elime/OneDrive/Documents/Keio/real world/testimages/20171009_090009.jpg");

        StartCoroutine(LoadImages());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.textures.Count > 0 && timeSinceLast > changeTime )
        {
            imageHolder.texture = textures[currentSlide];
            imageHolder.SizeToTexture();
            timeSinceLast = 0.0f;
            currentSlide = (currentSlide + 1) % textures.Count;
        }
        timeSinceLast += Time.deltaTime;
    }
}
