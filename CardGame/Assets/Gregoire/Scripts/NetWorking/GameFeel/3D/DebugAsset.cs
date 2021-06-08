using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AssetDebug", menuName = "Games/AssetDebug")]

public class DebugAsset : ScriptableObject
{
    [SerializeField]
    private string m_DebugString = string.Empty;
   public void DebugLog()
    {
        Debug.Log(m_DebugString);
    }
}
