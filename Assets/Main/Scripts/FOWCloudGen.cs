using BuildRTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWCloudGen : MonoBehaviour {

    public GameObject cloud;
    public int width = 10, height = 10;
    public int cloudSize = 5;

	// Use this for initialization
	void Start () {
        for (int x = -width; x < width; x+=cloudSize) {
            for (int y = -height; y < height; y+=cloudSize) {
                int groundLayerMask = 1 << 8;//Ground layer is layer 8
                Vector3 newPosition = new Vector3(transform.position.x + x, 0, transform.position.z + y);
                RaycastHit hit;
                float hitPoint = transform.position.y;
                if (Physics.Raycast(newPosition, new Vector3(0, 50, 0), out hit, Mathf.Infinity, groundLayerMask)) {
                    hitPoint = hit.point.y;
                }

                newPosition.y = hitPoint + Camera.main.GetComponent<RTSCamera>().minHeightFromGround - 5;

                GameObject cl = Instantiate(cloud, newPosition, cloud.transform.rotation);

                cl.name = "cloud_" + x + "_" + y;
                cl.transform.SetParent(transform);
            }
        }	
	}
	
}
