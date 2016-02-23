using UnityEngine;
using System.Collections;

public class DropTheMinions : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision");
        if(col.tag == "Enemy")
        {
            Debug.Log("dropping");
            col.gameObject.GetComponent<MinionController>().addGravity();
            col.GetComponent<MinionController>().DisableController();
        }
        
    }


}
