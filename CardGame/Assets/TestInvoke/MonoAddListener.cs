using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAddListener : MonoBehaviour
{
    public SO_TestInvoke m_TestInvoke = null;
    public GameObject m_CubeTest = null;
    public Material m_NewMat = null;
    // Start is called before the first frame update
    void Start()
    {
        m_TestInvoke.m_TestInvoke.AddListener(SwitchColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchColor()
    {
        Debug.Log("coucou");
        Material l_mat = m_CubeTest.GetComponent<MeshRenderer>().material;
        l_mat = m_NewMat;
        m_CubeTest.GetComponent<MeshRenderer>().material = l_mat;
    }
}
