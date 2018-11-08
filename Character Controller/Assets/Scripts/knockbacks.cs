using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbacks : MonoBehaviour {
    Vector3 velocity = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.MyCharacter.wallJumpLefting)
            velocity = new Vector3(1, 1, 0);
        else if(!GameManager.Instance.MyCharacter.wallJumpLefting)
            velocity = new Vector3(0, 0, 0); 
        velocity[0] = Mathf.Clamp(velocity[0], -1, 1);
        velocity[1] = Mathf.Clamp(velocity[1], -.5f, .5f);
        transform.position += velocity;
    }
}
