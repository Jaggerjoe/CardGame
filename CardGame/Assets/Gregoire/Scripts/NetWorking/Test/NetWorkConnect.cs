using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using MLAPI.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class NetWorkConnect : MonoBehaviour
{
    [SerializeField]
    private string m_IpAdress = "192.168.1.13";
    [SerializeField]
    private string m_IpAdressChanged;
    private int m_IpAdressPort = 7777;
    UNetTransport m_Transport;
    private SceneSwitchProgress m_SceneProgress;

    //private void OnGUI()
    //{
    //    if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
    //    {
    //        HostButton();
    //        JoinButton();

    //    }
    //    else
    //    {
    //        StatusLabels();
    //    }
    //}

    public void HostButton()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCallback;
        NetworkManager.Singleton.StartHost();
      //  NetworkSceneManager.SwitchScene("3DCardGame");
        //SceneManager.LoadScene("3DCardGame");

    }

    private void ApprovalCallback(byte[] p_ConnectionData, ulong p_ClientID, NetworkManager.ConnectionApprovedDelegate p_Callback)
    {
         bool l_Approve = System.Text.Encoding.ASCII.GetString(p_ConnectionData) == "passeword";
        p_Callback(true, null, l_Approve, Vector3.zero, Quaternion.identity);
    }

    public void JoinButton()
    {
        m_Transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        m_Transport.ConnectAddress = m_IpAdress;

        Debug.Log("join connect adress");

        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("passeword");
        NetworkManager.Singleton.StartClient();
       // m_SceneProgress.OnClientLoadedScene  += SceneProgress_OnClientLoadedScene;
    }

    private void SceneProgress_OnClientLoadedScene(ulong clientId)
    {

    }

    private void StatusLabels()
    {
        
        string mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    public void IPAdressChanged(string p_NewAdress)
    {
        this.m_IpAdress = p_NewAdress;
        Debug.Log("adress changed");
    }
    public void IPAdressChangedPort(int p_NewAdress)
    {
        this.m_IpAdressPort = p_NewAdress;
        Debug.Log("adress changed port");
    }

    private void StopDisconect()
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
/*   void OnGUI()
    {
        //taill et position des boutons
        GUILayout.BeginArea(new Rect(200, 200, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
           // StartButtons();

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
           // NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
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
    }*/
