using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullSpawner : MonoBehaviour
{
    public GameObject seagull;
    public Transform spawnPoint;
    public GameObject player;

    private int seagullCount;

    [SerializeField]
    private float spawningInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawningInterval));
    }

    private IEnumerator spawnEnemy(float interval)
    {
        if (seagullCount < 12 && player.transform.position.y > 6.5f && player.transform.position.y < 13.5f)
        {
            yield return new WaitForSeconds(interval);
            seagullCount++;
            Vector3 spawnArea = new Vector3(Random.Range(12, 120), spawnPoint.position.y, spawnPoint.position.z);
            GameObject newSeagull = Instantiate(seagull, spawnArea, spawnPoint.transform.rotation);
            StartCoroutine(spawnEnemy(interval));
        }
    }
}
