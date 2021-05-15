using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetWorkTestPlayer : NetworkBehaviour
{

    ulong m_test;
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
            //Ici on indique que seul le serveur à l'autorisation de déplacer la position 
            Position.Value = randomPosition;
           // Position.GetChannel();
           //Debug.Log("Get chanel : " +Position.GetChannel());
        }
        else
        {
            //si on est pas le serveur on va envoyer une demande au serveur via le ServerRpc
            SubmitPositionRequestServerRpc();
        }
    }


    /// <summary> ServerRpc=>
    /// on va bloquer la fonction sur le client 
    ///  et va indiquer dans la liste des choses qui est  le serveur
    /// à avoir une instance de ce joueur
    /// donc quand le joueur va demander un Rpc cette fonction est appellée mais sur le serveur avec son instance
    ///afin que seul le serveur puisse exécuté cette fonction et seulement lui executera la fonction
    ///car c'est un ServerRpc et non un Client Rpc
    ///
    /// </summary>
    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {

        //nous n avons pas besoin de deplacer l'objet physique donc pas besoin de faire transformer la position
        //car cette valeur Value va etre envoyer à nous lorsque  nous modifions les valeurs ici Position.Value 
        //Quand elle change elle sera diffuser a tous les clients
        //et si vous etes le host(execute le client et le serveur) le client recoit tjrs les choses changeante
        //donc tjrs recevoir le .Value update des infos
        Position.Value = GetRandomPositionOnPlane();
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Update()
    {
        //fait juste correspondre la position de l'objet à celle de la position du reseau
        transform.position = Position.Value;
    }
}
