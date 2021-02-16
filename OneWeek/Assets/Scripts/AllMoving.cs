using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMoving : MonoBehaviour {

    private float hDirection = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float nDir = Input.GetAxisRaw("Horizontal");

        if (hDirection != nDir) {
            hDirection = nDir;
            EvenController.current.MoveStatUpdate(hDirection);

        }

        if (Input.GetAxisRaw("Vertical") != 0) {
            EvenController.current.JumpAction();
        }

    }

}
