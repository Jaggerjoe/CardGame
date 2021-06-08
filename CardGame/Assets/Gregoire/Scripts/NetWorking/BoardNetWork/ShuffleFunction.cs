using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleFunction 
{
    public static void Shuffle(this IList _pIlist)
    {
        for (int i = 0; i < _pIlist.Count; i++)
        {
            int j = Random.Range(i, _pIlist.Count);
            Object l_Temp = (Object)_pIlist[i];
            _pIlist[i] = _pIlist[j];
            _pIlist[j] = l_Temp;
        }
    }
}
