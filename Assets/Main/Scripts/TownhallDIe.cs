using Lockstep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownHallDie : MonoBehaviour {
	
	void Update () {
        if (GameObject.Find("TownHall(Clone)(Clone)") == null && ResourceManager.wave > 1) {
            Debug.Log("You made it to wave " + ResourceManager.wave);
            ResourceManager.ResetData();
            Debug.Log(ResourceManager.wave);
            ClientManager.Quit();
            SceneManager.LoadScene("MainMenu");
        }
	}
}
