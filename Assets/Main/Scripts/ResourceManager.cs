using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static int minerals = 0;
    public static int lumber = 0;
	
	void updateMinerals(int m) {
        minerals += m;
    }

    void updateLumber(int l) {
        lumber += l;
    }
}
