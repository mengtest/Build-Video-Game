using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lockstep;
using UnityEngine.UI;

public class UnitBuildScript : Ability {

    [DataCode("Agents")]
    public string spawnAgentCode;
    public KeyCode hotkey;
    public int lumberCost = 50, mineralCost = 50, populationRequirement = 1;
    public GameObject buildableUnit;
    private bool canBuild = true;
	
	void Update () {
        canBuild = (ResourceManager.minerals >= mineralCost 
            && ResourceManager.lumber >= lumberCost 
            && ResourceManager.population + populationRequirement <= ResourceManager.maxPopulation);

        if (canBuild) {
            GetComponent<Image>().color = Color.white;
        } else {
            GetComponent<Image>().color = new Color32(35, 171, 162, 255);
        }

        if (Input.GetKeyDown(hotkey) && canBuild) {
            
            GameObject unit = Instantiate(buildableUnit);
            BuildableUnit bu = unit.GetComponent<BuildableUnit>();
            bu.spawnAgentCode = this.spawnAgentCode;
            bu.lumberCost = this.lumberCost;
            bu.mineralCost = this.mineralCost;
            
            
        }
	}
}
