using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardZoneEmplacement : MonoBehaviour
{
    [SerializeField]
    private EZoneCard.CardZones m_Zone = 0;

    public EZoneCard.CardZones Zone => m_Zone;
}
