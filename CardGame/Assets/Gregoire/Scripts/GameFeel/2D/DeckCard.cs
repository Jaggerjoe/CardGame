using MLAPI;
using MLAPI.NetworkVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCard : NetworkBehaviour
{
    [SerializeField]
    private List<SO_CardData> m_MyDeck = new List<SO_CardData>();

    [SerializeField]
    private GameObject m_Hand = null;

    [SerializeField]
    private GameObject m_CardPrefabAsset = null;

    // Start is called before the first frame update
     public override void NetworkStart()
    {
        CreateStartHand();
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    private void CreateStartHand()
    {
        if (IsServer)
        {
            m_Hand = GameObject.Find("BoardPlayer");
        }
        else
        {
            m_Hand = GameObject.Find("EnemBoard");

        }
        // m_Hand = GameObject.Find("HandPlayer");

        for (int i = 0; i < 6; i++)
        {
            Debug.Log(m_CardPrefabAsset);
            Debug.Log(m_Hand);
            Debug.Log(m_Hand.transform.position);

            GameObject l_CardAsset = Instantiate(m_CardPrefabAsset, m_Hand.transform.position,Quaternion.identity);
            Debug.Log(l_CardAsset);
            Debug.Log(m_Hand.transform.position);
            // l_CardAsset.GetComponent<NetworkObject>().Spawn();
            int l_Index = Random.Range(0, m_MyDeck.Count);
            l_CardAsset.GetComponent<DataCard>().Card = m_MyDeck[l_Index];
            m_MyDeck.RemoveAt(l_Index);
        }
    }

    public List<SO_CardData> CardList
    {
        get { return m_MyDeck; }
        set { m_MyDeck = value; }
    }
}
