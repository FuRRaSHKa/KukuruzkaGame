using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public GameObject first;
    public GameObject second;


    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.transform.tag == "Player") {


            first.SetActive(false);
            second.SetActive(true);
        
        }


    }


}
