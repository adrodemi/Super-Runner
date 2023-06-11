using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private int obstacleIndex;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
    void Update()
    {
        
    }
    private void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
    }
}