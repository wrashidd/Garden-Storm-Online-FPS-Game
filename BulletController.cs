using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Pun;

public class BulletController : MonoBehaviour
{
    //Shooting ************************************************
    public float moveSpeed,
        lifeTime;
    public Rigidbody theRB;
    public GameObject splashEffect;
    public int damage = 1;
    private int _addFruit = 1;

    public bool bDamageEnemy,
        bDemagePlayer;
    private PhotonView PV;

    //Pulling *************************************************

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BulletScalorRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && bDamageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }
        Destroy(gameObject);
        Instantiate(
            splashEffect,
            transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)),
            transform.rotation
        );
    }

    IEnumerator BulletScalorRoutine()
    {
        transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        theRB.velocity = transform.forward * moveSpeed;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
