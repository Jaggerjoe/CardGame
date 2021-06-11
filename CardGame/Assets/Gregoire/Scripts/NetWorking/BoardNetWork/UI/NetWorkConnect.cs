using UnityEngine;
using MLAPI;
using MLAPI.Connection;
using MLAPI.Transports.UNET;
using MLAPI.SceneManagement;
using UnityEngine.UI;


public class NetWorkConnect : MonoBehaviour
{
    [SerializeField]
    private string m_IpAdress = "192.168.1.13";
    [SerializeField]
    private string m_IpAdressChanged;
    [SerializeField]
    UNetTransport m_Transport;
    [SerializeField]
    private Button m_JoinButton; 
    [SerializeField]
    private Button m_HostButton;
    [SerializeField]
    private bool m_IsHostConnect =false;

    
    //public void OnGUI()
    //{
    //    using (new GUILayout.HorizontalScope())
    //    {

    //        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
    //        {
    //            GUILayout.Label("Not connected...");
    //            return;
    //        }
    //        else if (NetworkManager.Singleton.IsHost)
    //        {
    //            GUILayout.Space(800);
    //            using (new GUILayout.HorizontalScope())
    //            {
    //                using (new GUILayout.VerticalScope(GUI.skin.box))
    //                {
    //                    StatusLabels();
    //                }
    //            }
    //        }
    //    }
    //}

    //public void Start()
    //{
    //    Debug.Log("Je suis le buton la ");
    //    if (m_IsHostConnect == true)
    //    {
    //        m_JoinButton.interactable = true;
    //        m_HostButton.interactable = false;
    //    }
    //    m_IsHostConnect = true;
    //}

    public void HostButton()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCallback;
        NetworkManager.Singleton.StartHost();
        //m_IsHostConnect = true;
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
    }

    private void StatusLabels()
    {
        string mode = NetworkManager.Singleton.IsHost ?
                      "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
        

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Adress IP: " + m_Transport.ConnectAddress);
        GUILayout.Label("Mode: " + mode);

    }

    public void IPAdressChanged(string p_NewAdress)
    {
        this.m_IpAdress = p_NewAdress;
        Debug.Log("adress changed");
    }
   
    

    
}

