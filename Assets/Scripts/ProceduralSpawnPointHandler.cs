using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawnPointHandler : MonoBehaviour
{
    [SerializeField] WorldGeneration worldGeneration;
    [SerializeField] float playerHeight;
    private Vector3 _spawnPos;
    
    void Start()
    {
        playerHeight = GetComponentInChildren<SpriteRenderer>().bounds.size.y + 1;
        if (worldGeneration.spawnPoints != null && worldGeneration.spawnPoints.Length > 0)
        {
            _spawnPos = new Vector3((int)worldGeneration.spawnPoints[worldGeneration.worldSize / 2].x, (int)worldGeneration.spawnPoints[worldGeneration.worldSize / 2].y + playerHeight);
            transform.position = _spawnPos;
        }
    }
}
