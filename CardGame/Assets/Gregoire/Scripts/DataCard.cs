using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCard : MonoBehaviour
{
    [SerializeField]
    private SO_CardData m_DataCard = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateAssetCad()
    {

    }

    public SO_CardData Card
    {
        get { return m_DataCard; }
        set { m_DataCard = value; }
    }
}
