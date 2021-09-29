using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }

            return _instance;
        }
    }

    public Case activeCase;

    private void Awake()
    {
        _instance = this;
    }

    public void CreateNewCase()
    {
        activeCase = new Case();
        int generatedCaseNumber = Random.Range(0, 999);
        activeCase.caseID = generatedCaseNumber.ToString();
    }
}
