using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lockstep;
using BuildRTS;

public class BuildableUnit : Ability {

    [DataCode("Agents")]
    public string spawnAgentCode;
    public int lumberCost = 50, mineralCost = 50;
    public Material good, bad;
    private bool canBuild = true;


    void Update() {

        followMouse();

        RaycastHit sphereHit;
        if (Physics.SphereCast(transform.position, 30, new Vector3(1, 0, 1 ), out sphereHit, Mathf.Infinity, ~(1 << 8) )) {
            Debug.Log(sphereHit.collider.name);
            canBuild = false;
        } else {
            canBuild = true;
        }
        if (canBuild) {
            GetComponent<MeshRenderer>().material = good;
        } else {
            GetComponent<MeshRenderer>().material = bad;
        }

        if (Input.anyKeyDown) {
            if (!Input.GetMouseButtonDown(0)) {//if a key is pressed and its not the left mouse button, remove me
                gameObject.SetActive(false);
            } else if(canBuild) {
                if (ResourceManager.minerals >= mineralCost && ResourceManager.lumber >= lumberCost) {

                    long x = 0, z = 0;
                    FixedMath.TryParse(transform.position.x + "", out x);
                    FixedMath.TryParse(transform.position.z + "", out z);


                    GameObject.Find("TownHall(Clone)(Clone)").GetComponent<LSAgent>().Controller.CreateAgent(spawnAgentCode, new Vector2d(x, z));


                    ResourceManager.minerals -= mineralCost;
                    ResourceManager.lumber -= lumberCost;
                    gameObject.SetActive(false);

                } else {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void followMouse() {
        float height = transform.position.y;
        int groundLayerMask = 1 << 8;//Ground layer is layer 8
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 50, 0), out hit, Mathf.Infinity, groundLayerMask)) {
            height = hit.point.y;
        } else if (Physics.Raycast(transform.position, new Vector3(0, -50, 0), out hit, Mathf.Infinity, groundLayerMask)) {
            height = hit.point.y;
        }

        Vector3 mouseLoc = transform.position; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
            mouseLoc = hit.point;
        }

        transform.position = new Vector3(mouseLoc.x, height + 10, mouseLoc.z);
    }
}
