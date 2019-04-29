using Lockstep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    [DataCode("Agents")]
    public string spawnAgentCode;
    [DataCode("AgentControllers")]
    public string ControllerCode;

    public double spawnIntervalMinutes = 1;
    public double firstSpawnInterval = 1;


    private double time;

	void Start () {
        time = NanoTime;
	}

    void Update() {
        if(ResourceManager.wave == 1) {
            if (NanoTime - time >= firstSpawnInterval * 60 * 1000000000.0) {
                spawnZombies();
                time = NanoTime;
                ResourceManager.wave++;
            }
        } else {
            if (NanoTime - time >= spawnIntervalMinutes * 60 * 1000000000.0) {
                spawnZombies();
                time = NanoTime;
                ResourceManager.wave++;
            }
        }

    }
    public static long NanoTime {
        get { return (long)(System.Diagnostics.Stopwatch.GetTimestamp() / (System.Diagnostics.Stopwatch.Frequency / 1000000000.0)); }
    }

    void spawnZombies() {
        int numZombies = ResourceManager.wave * 3;
        var controller = AgentControllerHelper.Instance.GetInstanceManager(ControllerCode);
        long x = 0, z = 0;
        FixedMath.TryParse(transform.position.x + "", out x);
        FixedMath.TryParse(transform.position.z + "", out z);
        for (int i = 0; i < numZombies; i++) {
            controller.CreateAgent(spawnAgentCode, new Vector2d(x, z));
        }
    }
}
