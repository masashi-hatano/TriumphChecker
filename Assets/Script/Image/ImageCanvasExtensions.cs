using UnityEngine;
using UnityEngine.UI;

static class ImageCanvasExtensions
{
    public static Vector2 SizeToTexture(this RawImage image, float padding = 0.0f)
    {
        var imageTransform = image.GetComponent<RectTransform>();
        padding = 1 - padding;
        float w = 0, h = 0;
        float ratio = image.texture.width / (float)image.texture.height;
        var bounds = new Rect(imageTransform.anchoredPosition.x, imageTransform.anchoredPosition.y, imageTransform.rect.width, imageTransform.rect.height);
        if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
        {
            //Invert the bounds if the image is rotated
            bounds.size = new Vector2(bounds.height, bounds.width);
        }
        //Size by height first
        h = bounds.height * padding;
        w = h * ratio;
        if (w > bounds.width * padding)
        { //If it doesn't fit, fallback to width;
            w = bounds.width * padding;
            h = w / ratio;
        }
        imageTransform.sizeDelta = new Vector2(w, h);
        return imageTransform.sizeDelta;
    }
}