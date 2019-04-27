using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lockstep;

public class UnitBuildScript : Ability {

    [DataCode("Agents")]
    public string spawnAgentCode;
    public KeyCode hotkey;
    public int lumberCost = 50, mineralCost = 50;
    public GameObject buildableUnit;

	
	void Update () {
        if (Input.GetKeyDown(hotkey)) {
            if (ResourceManager.minerals >= mineralCost && ResourceManager.lumber >= lumberCost) {

                GameObject unit = Instantiate(buildableUnit);
                BuildableUnit bu = unit.GetComponent<BuildableUnit>();
                bu.spawnAgentCode = this.spawnAgentCode;
                bu.lumberCost = this.lumberCost;
                bu.mineralCost = this.mineralCost;
            
            }
        }
	}
}
