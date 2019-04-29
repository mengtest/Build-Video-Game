using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStatusBar : MonoBehaviour {

    public GameObject spawner;
	
	void Update () {
        GetComponent<RectTransform>().localScale = new Vector3(1, (float)spawner.GetComponent<ZombieSpawner>().percentTillWave, 1);
	}
}
