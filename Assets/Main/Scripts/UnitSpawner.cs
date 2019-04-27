﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lockstep;

namespace BuildRTS {
    public class UnitSpawner : BehaviourHelper {


        [SerializeField, DataCode("Input")]
        private string inputCode;
        [SerializeField, DataCode("Agents")]
        private string agentSpawnCode;


        public override ushort ListenInput {
            get {
                return InputCodeManager.GetCodeID(inputCode);
            }
        }


        protected override void OnVisualize() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                var com = GetSpawnCommand(PlayerManager.MainController, agentSpawnCode, RTSInterfacing.GetWorldPosD(Input.mousePosition));
                CommandManager.SendCommand(com);
            }
        }
        protected override void OnExecute(Command com) {
            ProcessSpawnCommand(com);
        }


        public Command GetSpawnCommand(AgentController controller, string agentCode, Vector2d position) {
            Command com = new Command();
            com.InputCode = InputCodeManager.GetCodeID(inputCode);
            com.ControllerID = controller.ControllerID;
            com.Add(new DefaultData(agentCode));
            com.Add(position);
            return com;
        }

        void ProcessSpawnCommand(Command com) {
            var controllerCode = com.ControllerID;
            var agentCode = (string) com.GetData<DefaultData>().Value;
            var position = com.GetData<Vector2d>();
            SpawnUnit(AgentController.GetInstanceManager(controllerCode), agentCode, position);
        }

        public LSAgent SpawnUnit (AgentController controller, string agentCode, Vector2d position) {
            LSAgent agent = controller.CreateAgent(agentCode, position);
            return agent;
        }
    }
}