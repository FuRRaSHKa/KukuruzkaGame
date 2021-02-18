using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornDrag : MonoBehaviour {

    float oldGravityScale;
    Rigidbody2D rgbd2d;
    MoovingScript moovingScript;
    bool isDrag = false;

    Vector2 direction = Vector2.zero;

    private void Start() {

        moovingScript = GetComponent<MoovingScript>();
        rgbd2d = GetComponent<Rigidbody2D>();

    }

    private void Update() {

        if (!isDrag)
            return;
        direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rgbd2d.position;


    }

    private void FixedUpdate() {
        if (!isDrag)
            return;
        //if (direction.magnitude > 7f) { 
        //    isDrag = false;
        //    rgbd2d.gravityScale = oldGravityScale;
        //    isDrag = false;
        //    return; 
        //}
        if (direction.magnitude < 1f) {
            rgbd2d.velocity = Vector2.zero;
            return;
        }
        rgbd2d.velocity = direction.normalized * 15;
    }

    private void OnMouseDown() {
        oldGravityScale = rgbd2d.gravityScale;
        isDrag = true;
        rgbd2d.gravityScale = 0;

    }

    private void OnMouseUp() {
        rgbd2d.gravityScale = oldGravityScale;
        isDrag = false;
        
    }

}
