using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhotoPanel : MonoBehaviour, IPanel
{
	public Text caseNumberText;
	public RawImage photoTaken;
	public InputField photoNotes;

	private string _imagePath;

	private void OnEnable()
	{
		caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseID;
	}
	public void ProcessInfo()
	{
		byte[] imageData = null;
        if (string.IsNullOrEmpty(_imagePath) == false)
        {
			//Grabs photo from the image path and converts to a png
			Texture2D image = NativeCamera.LoadImageAtPath(_imagePath, 512, false);
			imageData = image.EncodeToPNG();
		}

		//Displays image from image path
		UIManager.Instance.activeCase.photoTaken = imageData;
		UIManager.Instance.activeCase.photoNotes = photoNotes.text;
	}

	public void TakePictureButton()
    {
		TakePicture(512);
    }

    private void TakePicture(int maxSize)
	{
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				photoTaken.texture = texture;
				photoTaken.gameObject.SetActive(true);
				_imagePath = path;
			}
		}, maxSize);

	}
}
