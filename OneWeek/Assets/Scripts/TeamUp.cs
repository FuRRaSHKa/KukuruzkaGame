using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUp : MonoBehaviour {

    public List<TeamUpMooving> activeCorns;
    private GameObject[] allCorns;

    private int currenIndex;
    private bool isTeamUp = false;
    private Transform lowerBrick;

    private void Start() {

        activeCorns = new List<TeamUpMooving>();
        allCorns = GameObject.FindGameObjectsWithTag("Player");

    }

    private void Update() {

        

    }

    private void LateUpdate() {

        if (!isTeamUp) {
            if (Input.GetKeyDown(KeyCode.T)) {
                Debug.Log("Ke");
                Tower();

            }
        } else {



        }
        if (isTeamUp) {

            if (activeCorns[currenIndex].isComplete) {
                Debug.Log("Next");
                currenIndex++;
                if (currenIndex < activeCorns.Count)
                    lowerBrick = activeCorns[currenIndex].MoveToTower(lowerBrick, currenIndex, lowerBrick.GetComponent<Rigidbody2D>());
                else
                    isTeamUp = false;
            }

        }

    }

    public void noTower() {

        for (int i = 0; i < allCorns.Length; i++) {



        }

    }

    public void Tower() {

        if (activeCorns.Count != 0)
            activeCorns.Clear();

        for (int i = 0; i < allCorns.Length; i++) {
            WakeUpSamurai temp = allCorns[i].GetComponent<WakeUpSamurai>();
            if (temp != null) {
                if (temp.IsAlive) {
                    activeCorns.Add(allCorns[i].GetComponent<TeamUpMooving>());
                }
            }
        }

        for (int i = activeCorns.Count - 1; i > 0; i--) {

            int j = Random.Range(0, i);
            TeamUpMooving temp = activeCorns[j];
            activeCorns[j] = activeCorns[i];
            activeCorns[i] = temp;

        }


        lowerBrick = activeCorns[0].MoveToTower(null, 0, null);
        currenIndex = 1;
        isTeamUp = true;
        lowerBrick = activeCorns[currenIndex].MoveToTower(lowerBrick, currenIndex, lowerBrick.GetComponent<Rigidbody2D>());
    }

}
