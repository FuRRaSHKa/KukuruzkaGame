using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfView : MonoBehaviour {

    public GameObject[] allCorns;

    private bool firstTime = true;

    // Start is called before the first frame update
    void Start() {

        allCorns = GameObject.FindGameObjectsWithTag("Player");

        
    }

    // Update is called once per frame
    void Update() {

        if (firstTime) {
            firstTime = false;
            EvenController.current.SetPointOfVeiw(this.transform);
        }

        float sumY = 0, sumX = 0, activeCount = 0;

        for (int i = 0; i < allCorns.Length; i++) {
            WakeUpSamurai temp = allCorns[i].GetComponent<WakeUpSamurai>();
            
            if (temp != null) {
                
                if (temp.IsAlive) {
                      
                    sumX += allCorns[i].transform.position.x;
                    sumY += allCorns[i].transform.position.y;
                    activeCount++;
                }
            }

        }

        if (activeCount == 0)
            return;

        transform.position = new Vector3(sumX / activeCount, sumY / activeCount, 0);
    
    }

}
