using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleMovement : MonoBehaviour {


    System.Random rand = new System.Random();
    float n = 5f;
    float timer = .1f;
    Vector3 random3 = new Vector3();
    
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>();
        if (GameManager.Instance.MyCharacter.ismoveright)
        {
            random3 = new Vector3((rand.Next(-5, 0) / n), (rand.Next(0, 5) / n), 0);
        }
        if (GameManager.Instance.MyCharacter.ismoveleft)
        {
            random3 = new Vector3((rand.Next(0, 5) / n), (rand.Next(0, 5) / n), 0);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += random3;

        timer -= 1 * Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
