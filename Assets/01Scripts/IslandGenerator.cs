using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class IslandGenerator : MonoBehaviour
{

    public List<GameObject> rocks = new();
    private List<GameObject> spawnedRocks = new();
    public float size;
    
    private int rockCount = 100;
    [Button]
    void Go()
    {
        for (int i = 0; i < rockCount; i++)
        {
            var randPos = transform.position;
            randPos += new Vector3(Random.Range(-size, size), 0, Random.Range(-size, size));
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(size, 1, size));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
