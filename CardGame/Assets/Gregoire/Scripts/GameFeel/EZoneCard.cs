using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZoneCard : MonoBehaviour
{
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
}
