using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpSamurai : MonoBehaviour {

    public bool IsAlive = false;
    

    private Transform pointOfView;


    private void Awake() {
        
    }

    private void Start() {

        EvenController.current.onSetPointOfVeiw += SetPointOfVeiw;

        if (!IsAlive) {
            GetComponent<MoovingScript>().enabled = false;
        }

        

    }

    private void Update() {
   

        if (!IsAlive)
            return;

        if (pointOfView == null)
            return;

        if ((transform.position - pointOfView.transform.position).magnitude > Camera.main.orthographicSize) {
            GetComponent<MoovingScript>().enabled = false;
            IsAlive = false;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "PlayerTrigger" && !IsAlive && collision.transform.parent != transform) {

            if (!collision.transform.parent.GetComponent<WakeUpSamurai>().IsAlive)
                return;

            IsAlive = true;
            GetComponent<MoovingScript>().enabled = true;

        }
    }

    public void SetPointOfVeiw(Transform pointOfView) {
        this.pointOfView = pointOfView;

    }

    

}
