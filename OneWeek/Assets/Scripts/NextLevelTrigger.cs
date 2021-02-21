using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour {

    public int nextScene;
    public float delay;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.tag == "Player") {
        
        
        }

    }

    IEnumerator Loading() {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);

    }

}
