using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public static Collider2D groundC;
	// Use this for initialization
	void Start () {
        groundC = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
