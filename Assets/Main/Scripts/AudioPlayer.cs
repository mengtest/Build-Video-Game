using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioClip buildingDestroy;
    public AudioClip buildingCreate;
    public AudioClip zombieSpawn;

    public void playBuildingDestroy() {
        GetComponent<AudioSource>().PlayOneShot(buildingDestroy);
    }

    public void playBuildingCreate() {
        GetComponent<AudioSource>().PlayOneShot(buildingCreate);
    }

    public void playZombieSpawn() {
        GetComponent<AudioSource>().PlayOneShot(zombieSpawn);
    }
}
