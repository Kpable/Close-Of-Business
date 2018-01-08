using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Spawner
/// Just Spawns object, forever. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Spawner : MonoBehaviour {
    public float spawnRate;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
