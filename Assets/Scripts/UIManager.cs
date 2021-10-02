using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public void SubmitButton()
    {
        Case awsCase = new Case();
        awsCase.caseID = activeCase.caseID;
        awsCase.name = activeCase.name;
        awsCase.date = activeCase.date;
        awsCase.locationNotes = activeCase.locationNotes;
        awsCase.photoTaken = activeCase.photoTaken;
        awsCase.photoNotes = activeCase.photoNotes;

        //Save Case Data To A File
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/case#" + awsCase.caseID + ".dat";
        FileStream file = File.Create(filePath);
        bf.Serialize(file, awsCase);
        file.Close();

        //Send file to AWS
        AWSManager.Instance.UploadToAWS(filePath, awsCase.caseID);
    }
}
