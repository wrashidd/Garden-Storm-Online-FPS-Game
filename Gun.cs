﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Gun : Item
{
    public abstract override void Use();
    public GameObject fruitImpactPrefab;
}
