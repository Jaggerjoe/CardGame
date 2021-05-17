using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.Networking.Types;

public class NetWorkConnect : MonoBehaviour
{
    void OnGUI()
    {
        //taill et position des boutons
        GUILayout.BeginArea(new Rect(200, 200, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();

        }
        else
        {
            StatusLabels();
        }
       // if (GUILayout.Button("Disconeted")) StopDisconect();

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        
        if (GUILayout.Button("Host"))
        {
            NetworkManager.Singleton.StartHost();
            FindObjectOfType<NetWorkSpawnPlayer>().CreateBoardSOServerRpc();

        }
        if (GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.StartClient();
            //quand c'est lancer le client est deja instancier 
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
        
    }

    //appeler sur client et le host 
    private static void OnClientConnected(ulong clientId)
    {

        if (NetworkManager.Singleton.IsClient)
        {
            FindObjectOfType<NetWorkSpawnPlayer>().BindConnectPlayerClientRpc();
            Debug.Log("je bind ");

        }
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("je bind pas");
        }
    }
    static void StatusLabels()
    {
        if (GUILayout.Button("Disconeted")) StopDisconect();

        string mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void StopDisconect()
    {

        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StopHost();
          //  NetworkManager.Singleton.DisconnectClient();
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StopClient();
        }
        else if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StopServer();
        }
    }
}
