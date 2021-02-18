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
    private HingeJoint2D hingeJoint2D;
    private MoovingScript moovingScript;

    [HideInInspector]
    public bool isComplete = true;
    public bool teamedUp;

    private void Start() {
        moovingScript = GetComponent<MoovingScript>();
        rgbd2d = GetComponent<Rigidbody2D>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
        hingeJoint2D.enabled = false;
    }

    private void Update() {

        if (!isComplete && target != null) {
            if ((target.position + new Vector3(0, 3.5f, 0) - transform.position).magnitude <= minDistance) {
                rgbd2d.velocity = Vector2.zero;
                isComplete = true;
                Attach();
            }

            direction = (target.position + new Vector3(0, 3.5f, 0) - transform.position).normalized;



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

        hingeJoint2D.enabled = true;
        hingeJoint2D.connectedBody = rgbdToAttach;
        hingeJoint2D.anchor = new Vector2(-0.15f, -1.7f);
        UnlockAngle();
    }

    private void UnlockAngle() {
        if (rgbdToAttach == null) {
            rgbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        rgbd2d.freezeRotation = false;

    }

    private void LockAngle() {
       
        rgbd2d.constraints = RigidbodyConstraints2D.None;
        rgbd2d.freezeRotation = true;

    }

    /// <summary>
    /// Move Object to pos in tower
    /// </summary>
    /// <param name="target"> Target we moving for </param>
    /// <param name="index">Index of corn in corn tower</param>
    /// <param name="rgbd"> rigidbody we should attach to </param>
    public Transform MoveToTower(Transform target, int index, Rigidbody2D rgbdToAttach) {
        teamedUp = true;
        if (index == 0) {
            UnlockAngle();
            return transform;
        }

        this.target = target;
        this.rgbdToAttach = rgbdToAttach;
        isComplete = false;

        moovingScript.enabled = false;

        return transform;

    }



    public void NoAttach(int index) {

        if (index == 0) {
            LockAngle();
            return;
        }

        if (!teamedUp)
            return;

        teamedUp = false;
        isComplete = false;
        rgbdToAttach = null;
        target = null;
        hingeJoint2D.connectedBody = null;

        hingeJoint2D.enabled = false;
        moovingScript.enabled = true;

        rgbdToAttach = null;
        float angle = Random.Range(-89, 89);
        StartCoroutine(Rotate());
        rgbd2d.AddForce(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized * 160f, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (transform.rotation.eulerAngles.z != 0 && rgbdToAttach == null) {

            rgbd2d.AddForce(Vector2.up * 200f, ForceMode2D.Impulse);
           
        }
    }

    IEnumerator Rotate() {

        float angle = transform.eulerAngles.z;

        rgbd2d.angularVelocity = -angle * 1.5f;

        while (transform.eulerAngles.z != 0) {
            yield return null;
            if (transform.eulerAngles.z > -5 && transform.eulerAngles.z < 5) {
                transform.eulerAngles = Vector3.zero;
                rgbd2d.freezeRotation = true;
                yield break;
            }

        }


    }


}
