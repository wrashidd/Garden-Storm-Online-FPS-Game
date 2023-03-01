using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool bChasing;
    public float distanceToChase = 10f,
        distanceToLose = 15f,
        distanceToStop = 2f;

    private Vector3 targetPoint,
        startPoint;

    public NavMeshAgent agent;
    public float keepChasingTime = 5f;
    private float chaseCounter;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCount;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.Instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!bChasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                bChasing = true;
                fireCount = 1f;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
        }
        else
        {
            //transform.LookAt(targetPoint);
            //theRB.velocity = transform.forward * moveSpeed;
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                bChasing = false;
                chaseCounter = keepChasingTime;
            }

            fireCount -= Time.deltaTime;
            if (fireCount <= 0)
            {
                fireCount = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }
}
