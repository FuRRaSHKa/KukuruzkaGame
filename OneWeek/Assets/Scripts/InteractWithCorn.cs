using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCorn : MonoBehaviour {

    private SpringJoint2D springJoint2D;



    // Start is called before the first frame update
    void Start() {

        springJoint2D = GetComponent<SpringJoint2D>();

    }

    // Update is called once per frame
    void Update() {

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1)) {

           Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .5f);

            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].tag == "Player") {
                    springJoint2D.connectedBody = colliders[i].GetComponent<Rigidbody2D>();
                    
                    return;

                }
            
            }

        }

    }


}
