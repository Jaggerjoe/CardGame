using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TestInvoke", menuName = "Games/TestPerso")]

public class SO_TestInvoke : ScriptableObject
{
    public UnityEvent m_TestInvoke = null;

    public void TestInvoke()
    {
        m_TestInvoke.Invoke();
    }
}
