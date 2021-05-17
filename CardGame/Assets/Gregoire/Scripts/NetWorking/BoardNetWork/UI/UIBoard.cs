using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    public class UIBoard : MonoBehaviour
    {
        SO_Board m_SoBoard = null;

        private void Awake()
        {
            FindObjectOfType<SO_Board>().DropCardEvent.AddListener(ListenBoard);


        }
        //va recuperer les info et les appliquer à l'UI
        public void ListenBoard()
        {
        }
    }
}
