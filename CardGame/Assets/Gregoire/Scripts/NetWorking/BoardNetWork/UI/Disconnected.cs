using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.NetworkVariable;

public class Disconnected : MonoBehaviour
{

    public void StopDisconectHost()
    {

        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StopHost();
            //  NetworkManager.Singleton.DisconnectClient();
            Debug.Log("deconnecter");
        }
       
    }

    public void stopDisconectClient()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StopClient();
            Debug.Log("deconnecter");

        }
    }

}
