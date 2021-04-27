using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CreationCard : MonoBehaviour
{
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE= "A2:G23";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RequestGoogleSheet());
    }

    private IEnumerator RequestGoogleSheet()
    {
        UnityWebRequest request = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{SHEET_ID}/gviz/tq?tqx=out:csv&sheet={SHEET_NAME}&range={SHEET_RANGE}");

        yield return request.SendWebRequest();
        if(request.isHttpError ||request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            //Debug.Log(request.downloadHandler.data);
            //byte[] resukts = request.downloadHandler.data;
        }
    }
}
