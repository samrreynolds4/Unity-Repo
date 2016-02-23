using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float time = 2f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(AutoDelete());
	}
	
    IEnumerator AutoDelete()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
