using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DATACard", menuName = "CardGame/CardData")]
public class SO_CardData : ScriptableObject
{
    public string m_CardNames = string.Empty;

    public string m_EffectCardText = string.Empty;

    public int m_PointsCard = 0;

    public string m_Sign = string.Empty;

    public int m_PointCombo = 0;

    public EZoneCard.CardZones m_CardZone = 0;

    public SO_EffectCard m_Effect = null;

    public int m_Index = 0;
}

