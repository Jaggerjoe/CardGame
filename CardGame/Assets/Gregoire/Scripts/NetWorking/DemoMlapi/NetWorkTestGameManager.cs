using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class NetWorkTestGameManager : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200,200, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
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

    static void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            //on veut savoir qui se connect en premier et avoir son id
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                out var networkedClient))
            {
                //ont lui connect son clien en résesau, et aller chercher le composant sur l'objet Player
                var player = networkedClient.PlayerObject.GetComponent<NetWorkTestPlayer>();
                if (player)
                {
                     player.Move();
                    //Debug.Log("coucou je bouge");
                }
            }
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
