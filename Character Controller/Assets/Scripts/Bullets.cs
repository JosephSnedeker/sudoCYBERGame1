using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {
    [SerializeField]
    public Animator animator2;
    [SerializeField]
    GameObject bulletPrefab;
    public float look2 = 0;
    float timer = 3;
    [SerializeField]
    Vector3 velocity = new Vector3(4, 0, 0);
    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime * look2 ;
        if(look2 < 0)
        {
            animator2.SetBool("look", true);
        }
        if(look2 > 0)
        {
            animator2.SetBool("look", false);
        }
        
        timer -= 1 * Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }

    }
}
