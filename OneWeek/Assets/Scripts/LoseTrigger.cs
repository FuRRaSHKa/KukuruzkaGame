using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            if (collision.GetComponent<WakeUpSamurai>().isFirst) {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            }
        
        }

    }


}
