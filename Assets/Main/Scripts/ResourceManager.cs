using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static int minerals = 25;
    public static int lumber = 25;

    public static int population = 0;
    public static int maxPopulation = 0;


    public static int wave = 1;

    private void Update() {
        population = 0;
        maxPopulation = 0;
        Population[] pops = FindObjectsOfType<Population>();
        foreach (Population p in pops) {
            population += p.populationCost;
            maxPopulation += p.populationGain;
        }
    }

    void updateMinerals(int m) {
        minerals += m;
    }

    void updateLumber(int l) {
        lumber += l;
    }
}
