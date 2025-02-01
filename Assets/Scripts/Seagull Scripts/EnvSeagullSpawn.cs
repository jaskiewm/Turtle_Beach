using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvSeagullSpawn : MonoBehaviour
{
    public GameObject seagull;
    private int seagullCount;

    [SerializeField]
    private float spawningInterval;

    [SerializeField]
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnGull(spawningInterval));
    }
    private IEnumerator spawnGull(float interval)
    {
        if (seagullCount < 14)
        {
            seagullCount++;
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject newSeagull = Instantiate(seagull, spawnPoints[randSpawnPoint].position, transform.rotation);
            StartCoroutine(spawnGull(interval));
        }
        else if (seagullCount < 35)
        {
            yield return new WaitForSeconds(interval);
            seagullCount++;
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject newSeagull = Instantiate(seagull, spawnPoints[randSpawnPoint].position, transform.rotation);
            StartCoroutine(spawnGull(interval));
        }
    }
}
