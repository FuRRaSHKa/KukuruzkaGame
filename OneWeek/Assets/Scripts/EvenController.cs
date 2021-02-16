using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenController : MonoBehaviour {

    public static EvenController current;

    // Start is called before the first frame update
    void Awake() {

        if(current != null) {
            Destroy(this.gameObject);
        }

        current = this;

    }

    public event Action<float> onMoveStatUpdate;
    public void MoveStatUpdate(float dir) {

        if (onMoveStatUpdate != null) {
            onMoveStatUpdate(dir);
        }

    }

    public event Action onJumpAcrion;
    public void JumpAction() {
        if (onJumpAcrion != null) {
            onJumpAcrion();
        }
    }

}
