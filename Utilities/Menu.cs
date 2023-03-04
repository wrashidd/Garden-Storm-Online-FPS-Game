using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Main Menu
*/
public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
