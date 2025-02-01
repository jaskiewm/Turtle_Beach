using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SeagullMovement : MonoBehaviour
{
    [SerializeField] Rigidbody seagullBody;

    public NavMeshAgent seagull;
    private Transform player;
    public bool targetingPlayer;
    //public GameObject seagullIndicator;

    //private Transform spawnTarget;
    private float seagullVision = 20f;
    private float seagullPositionX;
    private float seagullPositionZ;
    private bool seagullBored=false;

    // Start is called before the first frame update
    void Start()
    {
        
        targetingPlayer = false;
        player = GameObject.Find("Player").transform;

        seagull = GetComponent<NavMeshAgent>();

        //seagullIndicator = GameObject.Find("SeagullIndicator");
        //seagullIndicator.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seagullPositionX = seagull.transform.position.x;
        seagullPositionZ = seagull.transform.position.z;
        float playerSeagullDiffX = Math.Abs(player.position.x - seagullPositionX);
        float playerSeagullDiffZ = Math.Abs(player.position.z - seagullPositionZ);

        Vector3 currectPos = seagull.transform.position;
        if (playerSeagullDiffX <= seagullVision && playerSeagullDiffZ <= seagullVision && seagullBored==false)
        {
            StartCoroutine(birdLosesInterest());
            diveBomb();
        }
            
        else if (currectPos.z < player.position.z - 15f)
            Destroy(gameObject);
        else
            seagull.SetDestination(new Vector3(currectPos.x, currectPos.y, -70));
    }

    private IEnumerator birdLosesInterest()
    {
        yield return new WaitForSeconds(3f);
        //seagullIndicator.SetActive(false);
        seagullBored = true;
    } 

    [ContextMenu("DiveBomb!")]
    public void diveBomb()
    {
        //seagullIndicator = GameObject.Find("SeagullIndicator");
        //seagullIndicator.SetActive(true);
        seagull.SetDestination(player.position);
        Vector3 direction = (player.position - seagullBody.position);
        direction = new Vector3(Mathf.Clamp(direction.x, -1f, 1f), -1, Mathf.Clamp(direction.z, -1f, 1f));
        seagullBody.velocity = direction * seagull.speed;
    }
}
