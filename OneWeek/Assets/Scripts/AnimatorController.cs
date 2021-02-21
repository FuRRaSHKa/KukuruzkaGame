using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    public GameObject eyes;
    public GameObject mouth; 
    public Animator anim;

    private bool isJump;
    private bool inJojo;

    // Start is called before the first frame update
    void Start() {



    }

    public void SetRunAnim(float dir) {

        if (dir == 0) {

            anim.SetBool("run", false);
            return;

        } 

        if(dir < 0) {

            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("run", true);
            eyes.transform.localPosition = new Vector3(eyes.transform.localPosition.x, eyes.transform.localPosition.y, 0.09f);
            mouth.transform.localPosition = new Vector3(mouth.transform.localPosition.x, mouth.transform.localPosition.y, 0.09f);

        }

        if (dir > 0) {

            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("run", true);
            eyes.transform.localPosition = new Vector3(eyes.transform.localPosition.x, eyes.transform.localPosition.y, -0.09f);
            mouth.transform.localPosition = new Vector3(mouth.transform.localPosition.x, mouth.transform.localPosition.y, -0.09f);


        }

    }

    public void SetJumpAnim() {
        if (isJump)
            return;
        anim.SetTrigger("jump");
        isJump = true;

    }

    public void SetInAir() {
        
        if (!isJump && !inJojo)
            return;
        anim.SetTrigger("inAir");
        inJojo = false;
        isJump = false;

    }

    public void JojoPos(bool jojoPos) {
        
        if (jojoPos)
            inJojo = true;

        anim.SetBool("jojoPos", jojoPos);

    }

}
