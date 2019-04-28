using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lockstep;
public class ZombieMove : MonoBehaviour {

	void Start () {
        LSAgent agent = GetComponent<LSAgent>();
        Vector2d gotoPos = Vector2d.zero;
        LSAgent[] allies = GameObject.FindObjectsOfType<LSAgent>();
        for (int i = 0; i < allies.Length; i++) {
            if (!allies[i].gameObject.name.Equals(gameObject.name)) {
                gotoPos = new Vector2d(FixedMath.Create((double)transform.position.x), FixedMath.Create((double)transform.position.z));
                break;
            }
        }
        agent.Controller.AddToSelection(agent);
        Command c = new Command(Lockstep.Data.AbilityDataItem.FindInterfacer<Scan>().ListenInputID);


        c.Add<Vector2d>(Vector2d.zero);
        c.SetFirstData<Selection>(new Selection(agent.Controller.SelectedAgents));
        c.ControllerID = agent.Controller.ControllerID;
        agent.Controller.SelectionChanged = false;

        CommandManager.SendCommand(c);
        agent.Controller.RemoveFromSelection(agent);

    }

    void Update () {
        


    }
}
