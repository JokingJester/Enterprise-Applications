using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour, IPanel
{
    public InputField caseNumberInput;
    public GameObject selectPanel;
    public void ProcessInfo()
    {
        AWSManager.Instance.GetList(caseNumberInput.text, () => 
        {
            selectPanel.SetActive(true);
        });
    }
}
