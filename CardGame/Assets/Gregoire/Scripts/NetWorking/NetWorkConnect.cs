using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.Networking.Types;

public class NetWorkConnect : NetworkBehaviour
{
    //static NetworkObject m_NetworkMana;

    //private void Start()
    //{
    //    m_NetworkMana.GetComponent<NetworkObject>();
    //}
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

           // m_NetworkMana = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

        }
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("je bind pas");
        }
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
