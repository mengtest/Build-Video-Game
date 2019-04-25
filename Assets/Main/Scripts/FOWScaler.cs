using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWScaler : MonoBehaviour {

    public float scaleSpeed = 5;
    public float maxSize = 5;
    public bool hasBeenSeen = false;
    private bool desiredState, doScaling = false;

	void Update () {
        if (doScaling) {
            if (desiredState == false) {
                Vector3 desiredSize = new Vector3(transform.localScale.x - Time.deltaTime * scaleSpeed, transform.localScale.y - Time.deltaTime * scaleSpeed, transform.localScale.z - Time.deltaTime * scaleSpeed);
                transform.localScale = desiredSize;

                if (transform.localScale.x <= 0.1) {
                    gameObject.SetActive(false);
                    doScaling = false;
                }
            } else {
                Vector3 desiredSize = new Vector3(transform.localScale.x + Time.deltaTime * scaleSpeed, transform.localScale.y + Time.deltaTime * scaleSpeed, transform.localScale.z + Time.deltaTime * scaleSpeed);
                transform.localScale = desiredSize;
                if (transform.localScale.x >= maxSize) {
                    transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                    gameObject.SetActive(true);
                    doScaling = false;
                }
            }
        }
	}
    private void LateUpdate() {
        hasBeenSeen = false;
    }

    public void toggleScale(bool setType) {
        desiredState = setType;
        doScaling = true;
    }
}
