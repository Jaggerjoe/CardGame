using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWork;

public class HandPlayer : MonoBehaviour
{
    [SerializeField]
    private int m_RowLenght = 6;

    [SerializeField]
    private float m_Space = 2;

    [SerializeField]
    private GameObject m_Prefab = null;


    [SerializeField]
    private Transform m_HandPlayer = null;

    [SerializeField]
    private SO_Board m_Board = null;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        for (int i = 0; i < m_RowLenght; i++)
        {
            GameObject l_Card =  Instantiate(m_Prefab, new Vector3(transform.position.x + (m_Space * (i % m_RowLenght)), 0.1f, transform.position.z), transform.rotation);
            l_Card.transform.SetParent(m_HandPlayer);
            l_Card.GetComponent<DataCard>().Card = m_Board.Side.m_Deck[0];
            m_Board.Side.m_Hand.Add(m_Board.Side.m_Deck[0]);
            m_Board.Side.m_Deck.RemoveAt(0);
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < m_Board.Side.m_Deck.Count; i++)
        {
            int j = Random.Range(i, m_Board.Side.m_Deck.Count);
            SO_CardData l_Temp = m_Board.Side.m_Deck[i];
            m_Board.Side.m_Deck[i] = m_Board.Side.m_Deck[j];
            m_Board.Side.m_Deck[j] = l_Temp;
        }
    }
}
