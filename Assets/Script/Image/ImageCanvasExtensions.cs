using UnityEngine;
using UnityEngine.UI;

static class ImageCanvasExtensions
{
    public static Vector2 SizeToTexture(this RawImage image, float padding = 0.0f)
    {
        var imageTransform = image.GetComponent<RectTransform>();
        float ratio = image.texture.width / (float)image.texture.height;
        imageTransform.sizeDelta = new Vector2(imageTransform.rect.width, imageTransform.rect.width*ratio);
        return imageTransform.sizeDelta;
    }
}