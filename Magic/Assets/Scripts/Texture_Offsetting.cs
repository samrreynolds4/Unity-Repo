using UnityEngine;
using System.Collections;

public class Texture_Offsetting : MonoBehaviour {

    public float scrollSpeed;
    Renderer mainMaterial;

    void Start()
    {
        mainMaterial = GetComponent<Renderer>();
    }

	void Update ()
    {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offSet = new Vector2(-1.3f, y);
        mainMaterial.material.mainTextureOffset = offSet;
	}
}
