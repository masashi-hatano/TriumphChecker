using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Globalization;

public class DownloadExif : MonoBehaviour
{

	public Texture2D texture = null;
	public string imagePath;
	private Texture2D newTexture;
	private string orientationString;
	public DateTime time_big;
	public DateTime time_end;
	public Text text;
	public int j = 0;
	public int k = 0;

	public IEnumerator LoadTexture()
	{
		yield return StartCoroutine(LoadByteArrayIntoTexture(this.imagePath));
		if (this.texture != null)
		{
			CorrectRotation();
		}
	}

	IEnumerator LoadByteArrayIntoTexture(string url)
	{
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
			text.text = url + www.error.ToString();
		}
		else
		{
			// retrieve results as binary data
			byte[] results = www.downloadHandler.data;

			Debug.Log("Finished Getting Image -> SIZE: " + results.Length.ToString());
			ExifLib.JpegInfo jpi = ExifLib.ExifReader.ReadJpeg(results, "Sample File");

			j++;
			text.text = j.ToString() + " " + jpi.DateTime;

			DateTime parsedDate;

			try
			{
				CultureInfo provider = CultureInfo.InvariantCulture;
				string format = "yyyy:MM:dd HH:mm:ss";
				parsedDate = DateTime.ParseExact(jpi.DateTime, format, provider);
			}
			catch (System.Exception e)
            {
				parsedDate = this.time_end;
			}

			if (parsedDate >= this.time_big && parsedDate < this.time_end)
			{
				Texture2D tex = new Texture2D(2, 2);
				tex.LoadImage(results);
				newTexture = tex;

				// Not sure why, but many images come in flipped 180 degrees
				//newTexture = rotateTexture(newTexture, true); // Rotate clockwise 90 degrees
				//newTexture = rotateTexture(newTexture, true); // Rotate clockwise 90 degrees (again, to flip it)
				this.texture = newTexture;
			}
			else
            {
				this.texture = null;
            }
		}
	}

	public void CorrectRotation()
	{
		// tries to use the jpi.Orientation to rotate the image properly
		newTexture = this.texture;

		switch (orientationString)
		{
			case "TopRight": // Rotate clockwise 90 degrees
				newTexture = rotateTexture(newTexture, true);
				break;
			case "TopLeft": // Rotate 0 degrees...
				break;
			case "BottomRight": // Rotate clockwise 180 degrees
				newTexture = rotateTexture(newTexture, true);
				newTexture = rotateTexture(newTexture, true);
				break;
			case "BottomLeft": // Rotate clockwise 270 degrees (I think?)...
				newTexture = rotateTexture(newTexture, true);
				newTexture = rotateTexture(newTexture, true);
				break;
			default:
				break;
		}

		this.texture = newTexture;

	}

	Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
	{
		Color32[] original = originalTexture.GetPixels32();
		Color32[] rotated = new Color32[original.Length];
		int w = originalTexture.width;
		int h = originalTexture.height;

		int iRotated, iOriginal;

		for (int j = 0; j < h; ++j)
		{
			for (int i = 0; i < w; ++i)
			{
				iRotated = (i + 1) * h - j - 1;
				iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
				rotated[iRotated] = original[iOriginal];
			}
		}

		Texture2D rotatedTexture = new Texture2D(h, w);
		rotatedTexture.SetPixels32(rotated);
		rotatedTexture.Apply();
		return rotatedTexture;
	}
}
