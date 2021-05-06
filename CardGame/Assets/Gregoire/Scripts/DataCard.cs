using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCard : MonoBehaviour
{
    [SerializeField]
    private SO_CardData m_DataCard = null;

    [SerializeField]
    private Text m_TextCardName = null;

    // Start is called before the first frame update
    void Start()
    {
        m_TextCardName = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CreateAssetCad();
    }

    private void CreateAssetCad()
    {
        if (m_DataCard != null)
        {
            m_TextCardName.text = m_DataCard.m_CardNames;
        }
    }

    public SO_CardData Card
    {
        get { return m_DataCard; }
        set { m_DataCard = value; }
    }
}