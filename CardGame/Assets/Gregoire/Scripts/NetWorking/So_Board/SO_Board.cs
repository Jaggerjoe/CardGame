using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
public class SO_Board : ScriptableObject
{
    //va avoir 2 instances de side(coter)

    //Reçoit les RPC
    [SerializeField]
    private NetWorkSpawnPlayer m_NetworkPlayer = null;

    private void Awake()
    {
        m_NetworkPlayer = FindObjectOfType<NetWorkSpawnPlayer>();
    }
}
