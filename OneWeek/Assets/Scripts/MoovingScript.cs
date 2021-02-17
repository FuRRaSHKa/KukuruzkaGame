using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovingScript : MonoBehaviour {

    public float maxHorizontalSpeed = 5f;
    public float delay = 0f;
    public float jumpImpulse = 5f;
    public float walkStrange;
    public float stopSpeed;

    private Rigidbody2D rgbd;
    private float horizontalDirection = 0;
    private bool isJump; 
    private bool onEarth = true;

    // Start is called before the first frame update
    void Start() {

        rgbd = GetComponent<Rigidbody2D>();

        EvenController.current.onMoveStatUpdate += MoveStatChange;
        EvenController.current.onJumpAcrion += JumpAction;

    }

    private void Update() {
        
    }

    void FixedUpdate() {

        physicMove();

        Jump();

    }

    private void Jump() {

        if (!isJump)
            return;
        isJump = false;
        if (onEarth) {
            onEarth = false;
            
            rgbd.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }


    }

    private void physicMove() {

        if (horizontalDirection > 0) {

            if (rgbd.velocity.x >= maxHorizontalSpeed) {
                rgbd.velocity = new Vector2(maxHorizontalSpeed, rgbd.velocity.y);
            } else {
                rgbd.AddForce(Vector2.right * walkStrange, ForceMode2D.Impulse);
            }

        }

        if (horizontalDirection < 0) {

            if (rgbd.velocity.x <= -maxHorizontalSpeed) {
                rgbd.velocity = new Vector2(-maxHorizontalSpeed, rgbd.velocity.y);
            } else {
                rgbd.AddForce(Vector2.left * walkStrange, ForceMode2D.Impulse);
            }

        }

        if (horizontalDirection == 0) {

            if (!onEarth)
                return;

            if (rgbd.velocity.sqrMagnitude > 0.1) {

                rgbd.AddForce(Vector2.left * rgbd.velocity * stopSpeed, ForceMode2D.Impulse);

            } else {
                rgbd.velocity = new Vector2(0, rgbd.velocity.y);
            }


        }

    }





    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "obst" && collision.transform.position.y < transform.position.y) {

            onEarth = true;

        }
    }

    public void MoveStatChange(float dir) {

        StartCoroutine(Update(dir));

    }

    private IEnumerator Update(float dir) {

        yield return new WaitForSeconds(delay);

        horizontalDirection = dir;
    }

    public void JumpAction() {
        
        StartCoroutine(Jumping());

    }

    private IEnumerator Jumping() {

        yield return new WaitForSeconds(delay);
        isJump = true;

    }
}
