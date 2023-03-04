using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for managing main menu
*/
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField]
    private Menu[] _menus;

    private void Awake()
    {
        instance = this;
    }

    // Opens and Closes menu
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].menuName == menuName)
            {
                _menus[i].Open();
            }
            else if (_menus[i].open)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].open)
            {
                CloseMenu(_menus[i]);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
