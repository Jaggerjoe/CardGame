using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DATACard", menuName = "Games")]
public class SO_CardData : ScriptableObject
{
    public string m_CardNames = string.Empty;

    public string m_EffectCard = string.Empty;

    public int m_PointsCard = 0;

    public string m_Sign = string.Empty;

    public int m_PointCombo = 0;

    public int m_Zone = 0;
}
