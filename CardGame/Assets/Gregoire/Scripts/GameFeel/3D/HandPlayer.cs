using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlayer : MonoBehaviour
{
    [SerializeField]
    private int m_RowLenght = 6;

    [SerializeField]
    private float m_Space = 2;

    [SerializeField]
    private GameObject m_Prefab = null;

    [SerializeField]
    private List<SO_CardData> m_DeckCards = new List<SO_CardData>();

    [SerializeField]
    private Transform m_HandPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        for (int i = 0; i < m_RowLenght; i++)
        {
            GameObject l_Card =  Instantiate(m_Prefab, new Vector3(transform.position.x + (m_Space * (i % m_RowLenght)), 0.1f, transform.position.z), transform.rotation);
            l_Card.transform.SetParent(m_HandPlayer);
            l_Card.GetComponent<DataCard>().Card = m_DeckCards[0];
            m_DeckCards.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shuffle()
    {
        for (int i = 0; i < m_DeckCards.Count; i++)
        {
            int j = Random.Range(i, m_DeckCards.Count   );
            SO_CardData l_Temp = m_DeckCards[i];
            m_DeckCards[i] = m_DeckCards[j];
            m_DeckCards[j] = l_Temp;
        }
    }

    public List<SO_CardData> CardList
    {
        get { return m_DeckCards; }
        set { m_DeckCards = value; }
    }
}
