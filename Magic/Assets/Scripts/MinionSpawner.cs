using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionSpawner : MonoBehaviour {

    //Delay for spawning minions
    public float delay;
    public GameObject enemy;

    public void spawnEnemy()
    {
        Instantiate(enemy, transform.position + new Vector3(0f, 5.3f, 0f), Quaternion.identity);
        
    }
}
