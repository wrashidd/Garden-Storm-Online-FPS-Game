using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for enemy/bot health and damage logic
*/
public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5;

    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
