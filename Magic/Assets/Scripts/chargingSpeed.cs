using UnityEngine;
using System.Collections;

public class chargingSpeed : MonoBehaviour {

    public float speed = 1;

    private float factor = 20;
    public Vector3 d;
    public GameObject explosionPrefab;
    public ParticleSystem explosionSystem;
    public ParticleSystem flameSystem;
    public MeshRenderer shader;
    private Rigidbody rb;
    private bool destroyed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flameSystem = GetComponent<ParticleSystem>();
        explosionSystem = explosionPrefab.GetComponent<ParticleSystem>();
        shader = GetComponent<MeshRenderer>();
        explosionSystem.emissionRate = speed * 30;
        flameSystem.emissionRate = speed * 20;
        //flameSystem.startSpeed = speed * 30;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!destroyed)
        {
            rb.velocity = d * speed * factor;
        }
        
    }

    public void setSpeed(Vector3 dir, float n)
    {
        speed = n;
        d = dir;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            Debug.Log("damaged enemy");
            col.GetComponent<MinionHealth>().TakeDamage(speed);
        }

        Instantiate(explosionPrefab, transform.position - transform.forward*5, Quaternion.identity);
        factor = 0;
        destroyed = true;
        Destroy(rb);
        Destroy(GetComponent<SphereCollider>());
        shader.enabled = false;
        flameSystem.Stop();
        StartCoroutine(DestroySpell());
    }

    IEnumerator DestroySpell()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
