using FastCollections;
using Lockstep;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuildRTS {

    public class GUIInterfacer : MonoBehaviour {

        public GameObject HUD_Base;
        public GameObject profileImage;
        public Sprite fighterImage, emptyImage, townHallImage;

        private FastList<GameObject> guiElements;
        private int IMAGE_SIZE = 64;
        private FastList<LSAgent> selection;
        private float timePassed = 0, numTimePasses = 0;

        void Start() {
            guiElements = new FastList<GameObject>();
            selection = SelectionManager.BoxedAgents;
        }

        void Update() {
            clearGUI();
            if (selection.Count > 0) {
                LSAgent[] agents = selection.ToArray();
                Dictionary<string, int> selectionKVPS = new Dictionary<string, int>();

                foreach (LSAgent ag in agents) {
                    if (ag == null) {
                        continue;
                    }
                    if (selectionKVPS.ContainsKey(ag.gameObject.name)) {
                        selectionKVPS[ag.gameObject.name] += 1;
                    } else {
                        selectionKVPS.Add(ag.gameObject.name, 1);
                    }
                }

                string[] keys = new string[selectionKVPS.Count];
                selectionKVPS.Keys.CopyTo(keys, 0);

                
                //profileImage.GetComponent<Image>().sprite = getImage(keys[0].ToLower());
                int x = 0, y = 0;
                for (int i = 0; i < keys.Length; i++) {
                    drawUnitProfileImage(keys[i], selectionKVPS[keys[i]], x, y);
                    x += (int)(IMAGE_SIZE*2);
                }
            } else {
                //profileImage.GetComponent<Image>().sprite = getImage("empty");

            }


            timePassed += Time.deltaTime;
            if (timePassed > 1) {
                numTimePasses++;
                timePassed -= 1;
                Debug.Log(numTimePasses  + ": " + selection.Count);
            }
        }

        private void drawUnitProfileImage(string name, int amount, int x, int y) {

            GameObject image = new GameObject();
            image.transform.parent = profileImage.transform;
            image.name = "UI_" + name;

            image.AddComponent<Image>();
            RectTransform rectTransform = image.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(x, y, 0);
            rectTransform.sizeDelta = new Vector2(IMAGE_SIZE, IMAGE_SIZE);
            image.GetComponent<Image>().sprite = getImage(name);

            GameObject text = new GameObject();
            text.transform.parent = image.transform;
            text.name = "Amount: " + amount;
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = ""+amount;
            textComponent.font = (Font)Resources.Load("UIFONT");
            textComponent.fontSize = 20;
            textComponent.color = Color.black;
            rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(IMAGE_SIZE - 12, -IMAGE_SIZE + 12, 0);
            rectTransform.sizeDelta = new Vector2(IMAGE_SIZE, IMAGE_SIZE);

            guiElements.Add(image);
            guiElements.Add(text);
        }

        private void clearGUI() {
            foreach (GameObject gui in guiElements) {
                Destroy(gui);
            }
            guiElements.FastClear();
        }

        private Sprite getImage(string name) {
            name = name.ToLower();
            if (name.StartsWith("fighter")) {
                return fighterImage;
            }

            if (name.StartsWith("town")) {
                return townHallImage;
            }
            return emptyImage;
        }

    }
}