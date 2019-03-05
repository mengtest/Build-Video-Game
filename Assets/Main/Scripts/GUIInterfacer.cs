using FastCollections;
using Lockstep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildRTS {
    public class GUIInterfacer : MonoBehaviour {

        private FastList<LSAgent> selection;
        private float timePassed = 0, numTimePasses = 0;

        void Start() {
            selection = SelectionManager.BoxedAgents;
        }

        void Update() {
            selection = SelectionManager.BoxedAgents;
            timePassed += Time.deltaTime;
            if (timePassed > 1) {
                numTimePasses++;
                timePassed -= 1;
                Debug.Log(numTimePasses  + ": " + selection.Count);
            }
        }

    }
}