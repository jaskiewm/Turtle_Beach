using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnvSeagullMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody seagullBody;

    [SerializeField]
    public Transform[] spawnPoints;


    public NavMeshAgent seagull;
    private float seagullPositionX;
    private float seagullPositionZ;
    private int randSpawnTarget;

    // Start is called before the first frame update
    void Start() {
        seagull = GetComponent<NavMeshAgent>();
        randSpawnTarget = Random.Range(0, spawnPoints.Length);
        seagull.SetDestination(spawnPoints[randSpawnTarget].position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seagullPositionX = seagull.transform.position.x;
        seagullPositionZ = seagull.transform.position.z;

        float playerSeagullDiffX = Math.Abs(spawnPoints[randSpawnTarget].position.x - seagullPositionX);
        float playerSeagullDiffZ = Math.Abs(spawnPoints[randSpawnTarget].position.z - seagullPositionZ);

        if (playerSeagullDiffX < 20f && playerSeagullDiffZ <20f)
        {
            if (seagullPositionX > 75)
                randSpawnTarget = Random.Range(0, 4);
            else
                randSpawnTarget = Random.Range(4, spawnPoints.Length);

            seagull.SetDestination(spawnPoints[randSpawnTarget].position);
        }
    }
}
