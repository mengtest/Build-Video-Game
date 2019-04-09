using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillLumber : MonoBehaviour {

    private long time;
    public double millTimeSeconds;
    public int mineAmount;

    void Start() {
        time = NanoTime;
    }
    public static long NanoTime {
        get { return (long)(System.Diagnostics.Stopwatch.GetTimestamp() / (System.Diagnostics.Stopwatch.Frequency / 1000000000.0)); }
    }

    void Update() {
        //Mine
        if (NanoTime - time >= millTimeSeconds * 1000000000.0) {
            ResourceManager.lumber += mineAmount;
            time = NanoTime;
        }
    }
}
