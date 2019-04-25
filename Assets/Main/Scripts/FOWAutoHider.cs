using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWAutoHider : MonoBehaviour {

    private GameObject mapObj;
    public float desiredDistance = 150;
    public bool doScale = true;


    void Start() {
        mapObj = GameObject.Find("FowClouds");    
    }

    void Update () {
        dissapearCheck();
	}

    private void dissapearCheck() {
        foreach(Transform child in mapObj.transform) {

            float distance = Vector3.Distance(child.position, transform.position);

            if (child.GetComponent<FOWScaler>().hasBeenSeen == false) {
                if (distance < desiredDistance) {
                    child.GetComponent<FOWScaler>().hasBeenSeen = true;
                    if (child.gameObject.activeInHierarchy) {
                        if (doScale) {
                            child.GetComponent<FOWScaler>().toggleScale(false);
                        } else {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }


}
