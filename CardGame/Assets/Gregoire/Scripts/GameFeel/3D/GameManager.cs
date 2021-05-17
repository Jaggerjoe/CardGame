using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PartiePhase m_PartiePahse = PartiePhase.Mulligan;
    // Start is called before the first frame update
    void Start()
    {
        m_PartiePahse = PartiePhase.Mulligan;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
public enum PartiePhase
{
    Mulligan,
    Placement,
    Résolution
}
