using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class NetWorkingTest : MonoBehaviour
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

           //SubmitNewPosition();
        }

        GUILayout.EndArea();
    }
    static void StartButtons()
    {
        if (GUILayout.Button("Host"))  NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server"))
        {
            NetworkManager.Singleton.StartServer(); 

            //if(NetworkManager.Singleton.IsServer)
            //{ 
            //    NeworkSpawnPlayer(); 
            
            //}
            
        }
    }


    //définir le mode et le transport, fonction d'information
    static void StatusLabels()
    {

        //permet d'avoir le mode dans lequel on se trouve
        //si ce n'est pas host alors on regarde server et si aussi ce n'est pas le cas alors c'est clien
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        //text qui s'affiche apres avoir clicker sur un des boutons
        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
        
        //pour revenir en arriere et se deco
        if (GUILayout.Button("Disconeted")) StopDisconect();

    }
    
    static void NeworkSpawnPlayer()
    {
        Debug.Log("spawn");
        //si le joueur est connecter en local
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
            out var networkedClient))
        {
            //alors on va faire bouger le joueur avec le buton move
            var player = networkedClient.PlayerObject.GetComponent<NetWorkPlayer>();
            if (player)
            {
                player.Move();

            }
        }
    }
    static void SubmitNewPosition()
    {
        //si le bouton que j'appuie est Server alors le serveur aura comme buton soit move soit request position si ce n'es pas server
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change" ))
        {
            //si le joueur est connecter en local
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                out var networkedClient))
            {
                //alors on va faire bouger le joueur avec le buton move
                var player = networkedClient.PlayerObject.GetComponent<NetWorkPlayer>();
                if (player)
                {
                    player.Move();
                    
                }
            }
        }
    }

    static void StopDisconect()
    {

        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StopHost();
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
