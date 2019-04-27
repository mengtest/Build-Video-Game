using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGroundViewFinder : MonoBehaviour {

    private Camera camera;
    private float[,] viewLocations;
    private GameObject minimap;
    public GameObject[] lines;

    void Start () {
        minimap = GameObject.Find("MinimapImage");
        camera = Camera.main;
        viewLocations = new float[4, 2];
        lines = new GameObject[4];
	}
	
	void Update () {
        updatePositions();
        drawMinimapView();
    }

    void updatePositions() {
        int[,] positions = new int[,] {
                {0, 0},
                {Screen.width, 0 },
                {Screen.width, Screen.height },
                {0, Screen.height }
        };
        for (int i = 0; i < positions.Length / 2; i++) {
            RaycastHit hit;
            int groundLayerMask = 1 << 8;//Ground layer is layer 8
            Ray ray = camera.ScreenPointToRay(new Vector3(positions[i, 0], positions[i, 1], 0));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask)) {
                viewLocations[i, 0] = hit.point.x;
                viewLocations[i, 1] = hit.point.z;
            }
        }
    }

    void drawMinimapView() {
        for (int i = 0; i < viewLocations.Length / 2; i++) {
            float minimapWidth = minimap.GetComponent<RectTransform>().sizeDelta.x;
            float minimapHeight = minimap.GetComponent<RectTransform>().sizeDelta.y;

            float startX = (minimapWidth / 1024.0f) * (viewLocations[i, 0]);
            float startY = (minimapHeight / 1024.0f) * (viewLocations[i, 1]);

            float endX = (minimapWidth / 1024.0f) * (viewLocations[(i+1)%(viewLocations.Length / 2), 0]);
            float endY = (minimapHeight / 1024.0f) * (viewLocations[(i+1) % (viewLocations.Length / 2), 1]);

            if (lines[i] == null) {
                lines[i] = new GameObject("Minimap Line " + i);
                lines[i].layer = 5;
                Image img = lines[i].AddComponent<Image>();
                lines[i].transform.SetParent(minimap.transform);
                img.color = Color.gray;//new Color(1, 1, 1, 1);
            }

            RectTransform rectTransform = lines[i].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(1, 1);

            float xScale = Math.Abs(endX - startX) > Math.Abs(endY - startY) ? Math.Abs(endX - startX) : 1;
            float yScale = Math.Abs(endY - startY) > Math.Abs(endX - startX) ? Math.Abs(endY - startY) : 1;


            rectTransform.localScale = new Vector3(xScale, yScale);
            rectTransform.transform.localPosition = new Vector3((startX+endX)/2, (startY + endY) / 2, 0);

        }
    }
}
