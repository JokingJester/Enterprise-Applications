using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhotoPanel : MonoBehaviour, IPanel
{
    public Text caseNumberText;
    public RawImage photoTaken;
    public InputField photoNotes;

    private void OnEnable()
    {
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseID;
    }
    public void ProcessInfo()
    {
        
    }
}
