using Lockstep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroundScript : MonoBehaviour {

    public GameObject camera;
	
	
	void Update () {
        //Always be touching ground
        float height = transform.position.y;
        int groundLayerMask = 1 << 8;//Ground layer is layer 8
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 50, 0), out hit, Mathf.Infinity, groundLayerMask)) {
            height = hit.point.y;
        } else if (Physics.Raycast(transform.position, new Vector3(0, -50, 0), out hit, Mathf.Infinity, groundLayerMask)) {
            height = hit.point.y;
        }


        transform.SetPositionAndRotation(new Vector3(camera.transform.position.x, height, camera.transform.position.z), transform.rotation);
        
	}
}
