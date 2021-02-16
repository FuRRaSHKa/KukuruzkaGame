using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpSamurai : MonoBehaviour {

    public bool IsAlive = false;

    private void Start() {

        if (!IsAlive) {
            GetComponent<MoovingScript>().enabled = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player" && !IsAlive) {

            IsAlive = true;
            GetComponent<MoovingScript>().enabled = true;

        }
    }

}
