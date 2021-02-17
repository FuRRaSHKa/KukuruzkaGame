using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUpMooving : MonoBehaviour {

    public float moveStrength;
    public float minDistance;
    public float maxSpeed = 5f;

    private Rigidbody2D rgbd2d;
    private Transform target;
    private Rigidbody2D rgbdToAttach;
    public Vector2 direction;
    private SpringJoint2D springJoint2D;
    private float grScale;
    private MoovingScript moovingScript;
    private bool specialLocked =false;

    [HideInInspector]
    public bool isComplete = true;
    public bool teamedUp;

    private void Start() {
        moovingScript = GetComponent<MoovingScript>();
        rgbd2d = GetComponent<Rigidbody2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
        springJoint2D.enabled = false;
        grScale = rgbd2d.gravityScale;
    }

    private void Update() {

        if (!isComplete && target != null) {
            if ((target.position + new Vector3(0, 3.5f, 0) -  transform.position ).magnitude <= minDistance) {

                isComplete = true;
                Attach();        
            }

            direction = (target.position + new Vector3(0, 3.5f, 0) - transform.position).normalized;



        }

        if (rgbdToAttach != null && teamedUp && !specialLocked) {
            if (!rgbdToAttach.freezeRotation && rgbd2d.freezeRotation) {
                Debug.Log(true);
                NoAttach();
            }
        }

    }

    private void FixedUpdate() {

        if (!isComplete && target != null) {
            rgbd2d.velocity = direction * maxSpeed;
            //if (rgbd2d.velocity.magnitude > maxSpeed) {
            //    rgbd2d.velocity = direction * maxSpeed;
            //} else {
            //    rgbd2d.AddForce(direction * moveStrength, ForceMode2D.Impulse);
            //}
        }

    }

    private void Attach() {

        rgbd2d.gravityScale = grScale;
        springJoint2D.enabled = true;
        springJoint2D.connectedBody = rgbdToAttach;
        springJoint2D.anchor = new Vector2(-0.15f, -1.7f);

    }

    /// <summary>
    /// Move Object to pos in tower
    /// </summary>
    /// <param name="target"> Target we moving for </param>
    /// <param name="index">Index of corn in corn tower</param>
    /// <param name="rgbd"> rigidbody we should attach to </param>
    public Transform MoveToTower(Transform target, int index, Rigidbody2D rgbdToAttach) {
        teamedUp = true;
        if (index == 0)
            return transform;

        this.target = target;
        this.rgbdToAttach = rgbdToAttach;
        isComplete = false;

        rgbd2d.gravityScale = 1;
        moovingScript.enabled = false;

        return transform;
        
    }

    public void UnlockAngle() {

        if (!teamedUp)
            return;

        if (rgbdToAttach == null) {
            rgbd2d.freezeRotation = true;
            rgbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        rgbd2d.freezeRotation = false;
        rgbdToAttach.GetComponent<TeamUpMooving>().UnlockAngle();
        
    }

    public void LockAngle() {

        if (!teamedUp)
            return;

        gameObject.layer = 0;

        if(rgbdToAttach == null) {
            teamedUp = true;  
            rgbd2d.constraints = RigidbodyConstraints2D.None;
            rgbd2d.freezeRotation = true;
            GetComponent<MoovingScript>().enabled = false;
            return;
        }

        rgbd2d.freezeRotation = true;
        rgbdToAttach.GetComponent<TeamUpMooving>().LockAngle();
        specialLocked = true;
    }

    public void NoAttach() {

        if (!teamedUp)
            return;
        
        teamedUp = false;
        isComplete = false;
        rgbdToAttach = null;
        target = null;
        springJoint2D.connectedBody = null;
        
        springJoint2D.enabled = false;
        moovingScript.enabled = true;

    }

}
