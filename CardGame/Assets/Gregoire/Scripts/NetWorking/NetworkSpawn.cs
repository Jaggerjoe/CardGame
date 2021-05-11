using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;


public class NetworkSpawn : NetworkBehaviour
{
    [SerializeField]
    private GameObject m_PrefaCard = null;
    [SerializeField]
    private GameObject m_SpawningPoint = null;

    private ulong m_NetIDUlong;

    [ClientRpc]
    public void SpawnningObjectClientRpc()
    {

        Debug.Log("IsLocalPlayer avant  " + IsLocalPlayer);
        if (IsLocalPlayer)
        {
            Debug.Log("IsLocalPlayer apres " + IsLocalPlayer);
            m_SpawningPoint = GameObject.Find("BoardPlayer");
            GameObject l_Go = Instantiate(m_PrefaCard, m_SpawningPoint.transform);
            l_Go.GetComponent<NetworkObject>().Spawn();
            Debug.Log("coucou je spawn avec le boardP");
        }
        else
        {
            Debug.Log("IsLocalPlayer avant  " + IsLocalPlayer);

            Debug.Log("IsLocalPlayer apres " + IsLocalPlayer);
            m_SpawningPoint = GameObject.Find("EnemBoard");
            //GameObject l_Go = Instantiate(m_PrefaCard, m_SpawningPoint.transform);
            //l_Go.GetComponent<NetworkObject>().Spawn();
            Debug.Log("coucou je spawn avec le BoardE");
        }


    }
}
