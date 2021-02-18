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

    }


    public void SetConnectedBody(Rigidbody2D connectedBody) {

        springJoint2D.connectedBody = connectedBody;

    }

}
