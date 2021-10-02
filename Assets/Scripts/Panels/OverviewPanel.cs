using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OverviewPanel : MonoBehaviour, IPanel
{
    public Text caseNumberTitle;
    public Text nameTitle;
    public Text dateTitle;
    public Text locationNotes;
    public RawImage photo;
    public Text photoNotes;

    private void OnEnable()
    {
        caseNumberTitle.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseID;
        nameTitle.text = UIManager.Instance.activeCase.name;
        dateTitle.text = DateTime.Now.ToString();
        locationNotes.text = "LOCATION NOTES: \n" + UIManager.Instance.activeCase.locationNotes;

        //Converting byte array to a raw image and displaying it
        Texture2D reconstructedImage = new Texture2D(1, 1);
        reconstructedImage.LoadImage(UIManager.Instance.activeCase.photoTaken);
        photo.texture = (Texture)reconstructedImage;
        photoNotes.text = "PHOTO NOTES: \n" + UIManager.Instance.activeCase.photoNotes;
    }
    public void ProcessInfo()
    {

    }
}
