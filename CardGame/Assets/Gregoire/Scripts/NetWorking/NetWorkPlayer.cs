using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetWorkPlayer : NetworkBehaviour
{
    /// <summary>
    ///  NetworkVariableSettings permet d'autoriser la synchronisation.
    ///  nous donne une autorisation d'ecriture (WritePermission) et de lecture (ReadPermission).
    ///  le champs de position du joueur est ecris par le serveur donc tout le monde peut le voir.
    ///  si en temps que client on se connect sur le serveur on ne peut pas modifier sa postion via 
    ///  un logiciel de piratage.
    ///  NetworkVariablePermission.Everyone met a jour tout le monde apres avoir lu le ServerOnly
    /// </summary>
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public override void NetworkStart()
    {
        Move();
    }

    public void Move()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }
        else
        {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = GetRandomPositionOnPlane();
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Update()
    {
        transform.position = Position.Value;
    }
}
