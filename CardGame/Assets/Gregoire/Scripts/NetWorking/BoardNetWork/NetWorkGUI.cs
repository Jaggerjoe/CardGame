using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Connection;

namespace NetWork
{
    public class NetWorkGUI : MonoBehaviour
    {

        private Board m_Board = null;
        private void OnGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                using (new GUILayout.VerticalScope())
                {
                    ConnectButton();
                }



            }

        }


        private void ConnectButton()
        {
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                if (GUILayout.Button("Start Host"))
                {
                    NetworkManager.Singleton.StartHost();
                   // Board.CreatAssetBoard();

                }
                if (GUILayout.Button("Start Client"))
                {
                    NetworkManager.Singleton.StartClient();

                }
                if (GUILayout.Button("Start Server")) NetworkManager.Singleton.StartServer();
            }
            else
            {
                // Displayy status
                var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

                GUILayout.Label("Transport: " +
                    NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
                GUILayout.Label("Mode: " + mode);

                GUILayout.Label("ID: " + NetworkManager.Singleton.LocalClientId);

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("ConnectBoard")) Board.ConnectBoard();

                }
                GUILayout.Space(20);
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Desconnected")) StopDisconect();
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

        private Board Board
        {
            get
            {
                if (m_Board == null)
                {
                    if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out NetworkClient l_Client))
                    {
                        m_Board = l_Client.PlayerObject.GetComponent<Board>();
                    }
                }
                else if (m_Board != null)
                {
                    Debug.Log("ahhhhhhh");
                }
                return m_Board;
            }
        }
    }
}