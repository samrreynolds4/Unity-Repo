using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    //Total nuber of enemies to start with
    public float count;

    //Number of enemies spawned
    public int spawned = 0;

    //UI Controller
    public SpellUI UI;

    //current Available enemies
    public float available;

    //what all the enemies health should be
    public float enemyHealth;

    //Spawn points of all enemies
    public MinionSpawner[] spawnPoints;

    //Amount allowed to be alive at one time
    public float amountAllowedAlive;

    //IS the controller spawning?
    bool spawning = false;

    //Random number
    int num;

    //Target for minions to go to
    public Transform target;

    //Static ref to this manager
    public static EnemyManager instance;

    //Delay for spawning enemies
    public float delay;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        available = count;
        //Scalling enemy health
        enemyHealth = (count * 4) / available;
        delay = ((spawned + 1) / available) * 60;
        Debug.Log("delay: " + delay);
    }

    void Update()
    {
        if (!spawning)
        {
            SpawnEnemy();
        }
    }
	
    public void SpawnEnemy()
    {

        if (available > 0 && spawned < amountAllowedAlive)
        {
            Debug.Log("Spawning Enemy");
            num = Random.Range(0, spawnPoints.Length - 1);
            spawning = true;
            StartCoroutine(Spawn(num));
        }
    }

    public IEnumerator Spawn(float n)
    {
        if (spawned < amountAllowedAlive)
        {
            spawned += 1;
            yield return new WaitForSeconds(delay);
            if (available > 0)
            {
                available -= 1;
                spawnPoints[(int)n].spawnEnemy();
                spawning = false;
            }
        }
    }

    public void EnemyRemoved()
    {
        spawned -= 1;

        //If no more enemies, end the game
        if (spawned < 1 && available < 1)
        {
            GameOver();
        }
    }

    public void EnemyDestroyed()
    {
        available += 1;
        spawned -= 1;
    }

    public void GameOver()
    {
        Debug.Log("GAMEOVER");
        UI.GameOver();
    }
}
