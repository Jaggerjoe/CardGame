using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCard : MonoBehaviour
{
    [SerializeField]
    private SO_CardData m_DataCard = null;

    [SerializeField]
    private TextMesh m_TextCardName = null;

    [SerializeField]
    private TextMesh m_Point = null;

    [SerializeField]
    private TextMesh m_Sign = null;

    [SerializeField]
    private TextMesh m_PointCombo = null;

    // Start is called before the first frame update
    void Start()
    {
        CreateAssetCad();
    }

    public void CreateAssetCad()
    {
        if (m_DataCard != null)
        {
            m_TextCardName.text = m_DataCard.m_CardNames;
            m_Point.text = m_DataCard.m_PointsCard.ToString();
            m_Sign.text = m_DataCard.m_Sign;
            m_PointCombo.text = m_DataCard.m_PointCombo.ToString();
        }
    }

    public SO_CardData Card
    {
        get { return m_DataCard; }
        set { m_DataCard = value; }
    }
}
