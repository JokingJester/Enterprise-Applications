using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientInfoPanel : MonoBehaviour, IPanel
{
    public Text caseNumberText;
    public Text errorMessage;
    public InputField firstName, lastName;

    [SerializeField] private GameObject _locationPanel;

    private void OnEnable()
    {
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseID;
    }
    public void ProcessInfo()
    {
        if (firstName.text.Length > 0 && lastName.text.Length > 0)
        {
            UIManager.Instance.activeCase.name = firstName.text + " " + lastName.text;
            errorMessage.gameObject.SetActive(false);
            _locationPanel.SetActive(true);
        }
        else
            errorMessage.gameObject.SetActive(true);
    }
}
