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

    public CardZones m_CardZone = 0;
}

[System.Flags]
public enum CardZones
{
    Zone1 = 1,
    Zone2 = 2,
    Zone3 = 4,
    Zone4 = 8,
    Zone5 = 16,
    Zone0 = 32

}