using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCorn : MonoBehaviour {

    private SpringJoint2D springJoint2D;

    private bool trigger = true;

    // Start is called before the first frame update
    void Start() {

        springJoint2D = GetComponent<SpringJoint2D>();

    }

    // Update is called once per frame
    void Update() {

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (trigger) {
            TriggerOn();
            
        } else {
            TriggerOff();
            
        }

    }

    void TriggerOn() {

        if (Input.GetMouseButtonDown(1)) {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .5f);

            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].tag == "Player") {

                    springJoint2D.connectedBody = colliders[i].GetComponent<Rigidbody2D>();
                    colliders[i].GetComponent<TeamUpMooving>().UnlockAngle();
                    trigger = false;
                    return;

                }

            }

        }


    }

    void TriggerOff() {

        if (Input.GetMouseButtonDown(1)) {

            springJoint2D.connectedBody.GetComponent<TeamUpMooving>().LockAngle();
            springJoint2D.connectedBody = null;
            trigger = true;
        }

    }

}
