using UnityEngine;
using System.Collections;

public class MinionHealth : MonoBehaviour {

    public float health;
    public EnemyManager manager;

	// Use this for initialization
	void Start ()
    {
        manager = EnemyManager.instance;
        health = manager.enemyHealth;
	}
	

    //take away health if player damaged minion
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            DestroyEnemy();
        }
    }

    //If player destroys enemy before they could kill themselves
    public void DestroyEnemy()
    {
        manager.EnemyDestroyed();
        Destroy(gameObject);
    }

    //If minion successfully kills themselves!
    public void RemoveEnemy()
    {
        manager.EnemyRemoved();
    }
}
