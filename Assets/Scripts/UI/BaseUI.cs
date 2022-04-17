using System;
using UnityEngine;

namespace UI
{
    public class BaseUI:MonoBehaviour
    {
        [SerializeField] protected GameObject mainMenu;
       
        public void Awake()
        {
            if (mainMenu == null)
            {
                mainMenu = FindObjectOfType<MainGUI>().gameObject;
            }
        }

    }
}