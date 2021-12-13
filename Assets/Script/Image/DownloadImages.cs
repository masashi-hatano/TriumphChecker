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
    public Image loadingBar;
    private List<Texture2D> textures = new List<Texture2D>();
    public float changeTime = 10.0f;
    private int currentSlide = 0;
    private float timeSinceLast = 1.0f;
    private List<string> galleryImages;
    private List<string> paths = new List<string>();

    public IEnumerator LoadImages()
    {
        System.Random rnd = new System.Random();
   
        loadingBar.gameObject.SetActive(true);
        loadingBar.fillAmount = 0f;
        int nb_images = this.galleryImages.Count();
        int counter = 0;

        foreach (string path in this.galleryImages)
        {
            counter++;
            loadingBar.fillAmount = (1f / nb_images) * counter;

            this.imagePath = path;
            yield return StartCoroutine(LoadTexture());
            if (this.texture != null)
            {
                this.paths.Add(path);
                this.textures.Add(this.texture);
            }
        }
        loadingBar.gameObject.SetActive(false);
        text.text = this.paths.Count().ToString();


        int l = 0;
        if (LoadScene.new_scene)
        {
            text.text = "Saved";
            LoadScene.SaveGame(this.paths);
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
        imageHolder.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

        this.time_big = LoadScene.time_big;
        this.time_end = LoadScene.time_end;

        //galleryImages = new List<string>();
        if (LoadScene.new_scene)
        {
            galleryImages = FindPaths.GetAllGalleryImagePaths();
        }
        else
        {
            galleryImages = LoadScene.paths_photos;
        }

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
