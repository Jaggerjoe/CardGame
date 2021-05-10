using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;


public class NetWorkConnect : NetworkBehaviour
{

    void OnGUI()
    {
        //taill et position des boutons
        GUILayout.BeginArea(new Rect(200, 200, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();

        }
        if (GUILayout.Button("Disconeted")) StopDisconect();

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host"))
        {
            NetworkManager.Singleton.StartHost();
            FindObjectOfType<NetworkSpawn>().SpawnningObjectClientRpc();
            
        }
        if (GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("youyoy");
            FindObjectOfType<NetworkSpawn>().SpawnningObjectClientRpc();
        }
        //if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
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
