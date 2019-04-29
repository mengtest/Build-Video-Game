using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanUnitHeal : MonoBehaviour {

    void Update() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6);
        foreach (Collider c in hitColliders) {
            Lockstep.Health hp;
            if (!c.gameObject.name.Equals(gameObject.name) && (hp = c.GetComponent<Lockstep.Health>()) != null) {
                if (hp.HealthAmount != hp.MaxHealth) {
                    hp.HealthAmount = hp.MaxHealth;
                    GetComponent<Lockstep.Health>().HealthAmount = 0;
                }
            }
        }

    }
}
