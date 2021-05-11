using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MLAPI;
using MLAPI.Messaging;

public class NetWorkSpawnPlayer : NetworkBehaviour
{
    [SerializeField]
    private ulong m_ClientId;



    private void Start()
    {

    }
    //il va creer le so_boad mais vide
    [ServerRpc]
    public void CreateBoardSOServerRpc()
    {
        //SO_Board l_BoardInstance = ScriptableObject.CreateInstance<SO_Board>();
        //string l_AssetPathName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Gregoire/Scripts/NetWorking/So_Board/NewBoard.asset");
        //AssetDatabase.CreateAsset(l_BoardInstance, l_AssetPathName);
       // Debug.Log("coucou je suis l'instance So creer");
        Debug.Log("IsLocalPlayer ServerRpc  = " + IsLocalPlayer);
    }

    //Les fonction auront les RPC pour avoir les infos


    //Les joueurs vont se faire spawn eux-meme
    public void SpawnPlayer()
    {

    }



    //bind(lier) qui est le J1 et qui est le J2
    [ClientRpc]
    public void BindConnectPlayerClientRpc()
    {
        Debug.Log("IsLocalPlayer ClientRpc  = " + IsLocalPlayer);

       // GetComponent<NetworkObject>().ChangeOwnership(m_ClientId);



        //m_NetworkMana =NetworkManager.Singleton.ConnectedClients[m_ClientId].PlayerObject;
        //Debug.Log("Network mana id =" + m_NetworkMana);
        // Debug.Log("client id =" + m_ClientId);
        //m_NetworkMana = NetworkManager.Singleton.LocalClientId[l_ClientId];

        if (IsLocalPlayer)
        {
            Debug.Log(" is local player J1");
        }
        else
        {
            Debug.Log(" is local player J2");
        }

        //if (IsOwner)
        //{
        //    Debug.Log(" youyou");
        //}
        //else
        //{
        //    Debug.Log("pas youyu");
        //}
    }
}