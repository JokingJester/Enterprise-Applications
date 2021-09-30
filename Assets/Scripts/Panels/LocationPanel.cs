using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LocationPanel : MonoBehaviour, IPanel
{
    public RawImage map;
    public Text caseNumberText;
    public InputField mapNotes;

    public string apiKey;
    public float xCoord, yCoord;
    public int zoom, imageSize;
    public string url = "https://maps.googleapis.com/maps/api/staticmap?";

    private void OnEnable()
    {
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseID;
    }
    private IEnumerator Start()
    {
        if(Input.location.isEnabledByUser == true)
        {
            Input.location.Start();

            int maxWait = 20;
            while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if(maxWait < 1)
            {
                Debug.Log("Timed Out");
                yield break;
            }

            if(Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
            }
            else
            {
                xCoord = Input.location.lastData.latitude;
                yCoord = Input.location.lastData.longitude;
            }

            Input.location.Stop();
        }
        else
        {
            Debug.Log("Location Service Deactivated");
        }

        StartCoroutine(DownloadGoogleMapRoutine());
    }

    private IEnumerator DownloadGoogleMapRoutine()
    {
        url = url + "center=" + xCoord + "," + yCoord + "&zoom=" + zoom + "&size=" + imageSize + "x" + imageSize + "&key=" + apiKey;
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                map.texture = texture;
            }
        }

    }

    public void ProcessInfo()
    {
        if(string.IsNullOrEmpty(mapNotes.text) == false)
        {
            UIManager.Instance.activeCase.locationNotes = mapNotes.text;
        }
    }
}
