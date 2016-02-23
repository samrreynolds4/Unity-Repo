using UnityEngine;
using System.Collections;

public class AreaSpell : MonoBehaviour {

    public float radius;
    public float damage = 20f;
    

	void Start()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].tag == "Enemy")
            {
                hitColliders[i].GetComponent<MinionHealth>().TakeDamage(damage);
                hitColliders[i].GetComponent<Rigidbody>().AddForce(-(hitColliders[i].transform.position - transform.position) * 5);
            }
            i++;
        }
    }
    
}
