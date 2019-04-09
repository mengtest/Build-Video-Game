using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMinerals : MonoBehaviour {

    public long time;
    public double mineTimeSeconds;
    public int mineAmount;

	void Start () {
        time = NanoTime;
	}
    public static long NanoTime {
        get { return (long)(System.Diagnostics.Stopwatch.GetTimestamp() / (System.Diagnostics.Stopwatch.Frequency / 1000000000.0)); }
    }

    void Update () {
        //Mine
        if (NanoTime - time >= mineTimeSeconds*1000000000.0) {
            ResourceManager.minerals += mineAmount;
            time = NanoTime;
        }
    }
}
