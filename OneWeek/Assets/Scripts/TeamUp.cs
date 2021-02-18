using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeamUp : MonoBehaviour {

    public List<TeamUpMooving> activeCorns;
    private GameObject[] allCorns;

    private int currenIndex;
    private bool isTeamUp = false;
    private Transform lowerBrick;
    private float oldCamSize;

    private void Start() {

        activeCorns = new List<TeamUpMooving>();
        allCorns = GameObject.FindGameObjectsWithTag("Player");
        oldCamSize = Camera.main.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
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

            if (Input.GetKeyDown(KeyCode.T)) {
                Debug.Log("ne");
                UnTower();

            }

        }

    }

    public void UnTower() {

        for (int i = 0; i < activeCorns.Count; i++) {

            activeCorns[i].NoAttach(i);

        }
        isTeamUp = false;
        StartCoroutine(ChangeCamSize(oldCamSize));

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

        float sumY = 0, sumX = 0, activeCount = 0;
        Vector2 pos;

        for (int i = 0; i < activeCorns.Count; i++) {

            sumX += allCorns[i].transform.position.x;
            sumY += allCorns[i].transform.position.y;
            activeCount++;

        }

        if (activeCount != 0) {
            pos = new Vector3(sumX / activeCount, sumY / activeCount, 0);
            int id = 0;
            float minDist = (pos - (Vector2)activeCorns[0].transform.position).sqrMagnitude;

            for (int i = 1; i < activeCorns.Count; i++) {
                float mag = (pos - (Vector2)activeCorns[i].transform.position).sqrMagnitude;
                if (mag < minDist) {
                    id = i;
                }

            }
            if (id != 0) {
                TeamUpMooving temp = activeCorns[0];
                activeCorns[0] = activeCorns[id];
                activeCorns[id] = temp;
            }
        }



        for (int i = activeCorns.Count - 1; i > 0; i--) {

            int j = Random.Range(1, i);
            TeamUpMooving temp = activeCorns[j];
            activeCorns[j] = activeCorns[i];
            activeCorns[i] = temp;

        }

        isTeamUp = true;

        if (activeCorns.Count > 5) {

            StartCoroutine(ChangeCamSize(activeCorns.Count * 2.5f));

        }

        lowerBrick = activeCorns[0].MoveToTower(null, 0, null, false);
        currenIndex = 1;
        //lowerBrick = activeCorns[currenIndex].MoveToTower(lowerBrick, currenIndex, lowerBrick.GetComponent<Rigidbody2D>());

        for (currenIndex = 1; currenIndex < activeCorns.Count; currenIndex++) {
            lowerBrick = activeCorns[currenIndex].MoveToTower(lowerBrick, currenIndex, lowerBrick.GetComponent<Rigidbody2D>(), currenIndex == activeCorns.Count-1 ? true : false);
        }

        

    }

    IEnumerator ChangeCamSize(float size) {
        CinemachineVirtualCamera cinemachineVirtualCamera = Camera.main.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        float oldSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;

        if (oldSize == size) {
            Debug.Log("ye");
            yield break;
        }
       
        if (size > oldSize) {
            while (cinemachineVirtualCamera.m_Lens.OrthographicSize < size) {

                if (!isTeamUp)
                    yield break;

                cinemachineVirtualCamera.m_Lens.OrthographicSize += (size - oldSize) * Time.deltaTime / 2;
                yield return null;

            }
        } else {
            while (cinemachineVirtualCamera.m_Lens.OrthographicSize > size) {

                if (isTeamUp)
                    yield break;

                cinemachineVirtualCamera.m_Lens.OrthographicSize += (size - oldSize) * Time.deltaTime / 2;
                yield return null;

            }

        }
    }

  

}
