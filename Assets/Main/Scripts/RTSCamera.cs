using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BuildRTS {
    public class RTSCamera : MonoBehaviour {

        public float scrollSpeed = 15;
        public float zoomDelta = 35;
        public float edgeMargin = 5;
        public float minZoom = 0.5f;
        public float maxZoom = 2.0f;
        public float dragSpeed = 200;
        public int minHeightFromGround = 20;
        public GameObject groundLocator;

        private Vector3 dragOrigin;
        private float baseHeight, currentZoom;
        private bool isCameraDragging = false;
        private Vector3 camStartPos;
        private Quaternion camStartRotation;

        void Start() {
            baseHeight = transform.position.y;
            currentZoom = 1;
        }


        void Update() {
            //Edgepan
            Vector3 moveMouse = new Vector3(0, 0, 0);

            if (Input.mousePosition.x <= 0 + edgeMargin) {
                moveMouse.x = -scrollSpeed;
            } else if (Input.mousePosition.x >= Screen.width - edgeMargin) {
                moveMouse.x = scrollSpeed;
            }

            if (Input.mousePosition.y <= 0 + edgeMargin) {
                moveMouse.z = -scrollSpeed;
            } else if (Input.mousePosition.y >= Screen.height - edgeMargin) {
                moveMouse.z = scrollSpeed;
            }
            moveMouse *= Time.deltaTime;

            transform.position += moveMouse;

            //Zoom in/out
            float zoomInput = Input.GetAxis("Zoom");
            zoomInput *= zoomDelta;
            Vector3 zoom = new Vector3(0, 0, zoomInput);
            transform.Translate(zoom, Space.Self);

            //Middle Mouse 
            if (Input.GetButtonDown("MiddleMouse")) {
                dragOrigin = Input.mousePosition;
                isCameraDragging = true;
                camStartPos = transform.position;
                camStartRotation = transform.rotation;
            }
            if (Input.GetButtonUp("MiddleMouse")) {
                isCameraDragging = false;
                camStartPos = transform.position;
                camStartRotation = transform.rotation;
            }
            if (!Input.GetButton("MiddleMouse")) {
                isCameraDragging = false;
            }
            if (isCameraDragging) {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                Vector3 move = new Vector3(pos.x * -dragSpeed, 0, pos.y * -dragSpeed);

                transform.SetPositionAndRotation(camStartPos, camStartRotation);
                transform.Translate(move, Space.Self);
                transform.SetPositionAndRotation(new Vector3(transform.position.x, camStartPos.y, transform.position.z), transform.rotation);
            }

            //Make sure at minimum height
            if (this.gameObject.transform.position.y - groundLocator.transform.position.y <= minHeightFromGround) {
                this.gameObject.transform.SetPositionAndRotation(new Vector3(this.gameObject.transform.position.x, groundLocator.transform.position.y + minHeightFromGround, this.gameObject.transform.position.z), transform.rotation);
            }
        }
    }
}