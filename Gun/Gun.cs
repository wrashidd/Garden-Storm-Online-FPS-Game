using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
This class derives from parent Item class that is responsible for players inventory
*/
public abstract class Gun : Item
{
    public abstract override void Use();
    public GameObject fruitImpactPrefab;
}
