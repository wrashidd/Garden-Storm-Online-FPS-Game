using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for player's gun inventory system
*/
public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public GameObject itemGameObject;

    public abstract void Use();
}
