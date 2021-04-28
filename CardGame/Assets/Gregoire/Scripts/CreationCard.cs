using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


public class CreationCard : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(RequestGoogleSheet());

    }

    

    //public IEnumerator RequestGoogleSheet()
    //{
    //    UnityWebRequest request = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{SHEET_ID}/gviz/tq?tqx=out:csv&sheet={SHEET_NAME}&range={SHEET_RANGE}");

    //    yield return request.SendWebRequest();
    //    if(request.isHttpError ||request.isNetworkError)
    //    {
    //        Debug.Log(request.error);
    //    }
    //    else
    //    {
    //        Debug.Log(request.downloadHandler.text);
    //        //Debug.Log(request.downloadHandler.data);
    //        //byte[] resukts = request.downloadHandler.data;
    //    }
    //}
}
