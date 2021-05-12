using MLAPI;
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
    void Start()
    {
        CreateStartHand();
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    private void CreateStartHand()
    {
        //if(IsLocalPlayer)
        //{

        //}
        //else
        //{
        //    m_Hand = GameObject.Find("EnemBoard");

        //}
        m_Hand = GameObject.Find("HandPlayer");

        for (int i = 0; i < 6; i++)
        {
            GameObject l_CardAsset = Instantiate(m_CardPrefabAsset, m_Hand.transform.position,Quaternion.identity);
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
