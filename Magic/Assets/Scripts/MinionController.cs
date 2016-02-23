using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    public Transform target;
    private Vector3 direction;
    public float speed;
    public float step;
    public Rigidbody rb;
    public MinionHealth healthManager;
    private Quaternion lookRotation;
    public float rotationSpeed = 4f;
    public bool enable = true;

    void start()
    {
        healthManager = GetComponent<MinionHealth>();
        // speed = Random.Range(1, healthManager.speed/5);
        speed = Random.Range(5, 10);
        target = EnemyManager.instance.target;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (enable)
        {
            step = speed * Time.deltaTime;
            direction = (target.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, 1 / (speed * (Vector3.Distance(gameObject.transform.position, target.position))));
        }

    }

    public void DisableController()
    {
        enable = false;
        EnemyManager.instance.EnemyRemoved();
        StartCoroutine(Delete());
    }

    public IEnumerator Delete()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    public void addGravity()
    {
        
        rb.useGravity = true;
        rb.mass = 10;
        rb.isKinematic = false;
        rb.AddForce(transform.forward*200 + new Vector3(0, 10, 0) * 400);

    }
}
