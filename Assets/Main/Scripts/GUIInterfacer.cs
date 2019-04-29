using FastCollections;
using Lockstep;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace BuildRTS {

    public class GUIInterfacer : MonoBehaviour {

        public GameObject HUD_Base;
        public GameObject profileImage;
        public Sprite fighterImage, emptyImage, townHallImage, mineralImage, lumberImage;
        public GameObject playerResources;


        private Dictionary<string, FastList<LSAgent>> selectionUnits;
        private FastList<GameObject> guiElements;
        private int IMAGE_SIZE = 64;
        private FastList<LSAgent> selection;

        private long time;
        private double delta;
        private readonly int fps = 60;

        void Start() {
            delta = 0;
            time = NanoTime;
            guiElements = new FastList<GameObject>();
            selection = SelectionManager.BoxedAgents;
        }
        public static long NanoTime {
            get { return (long)(Stopwatch.GetTimestamp() / (Stopwatch.Frequency / 1000000000.0)); }
        }
        void Update() {
            
            delta += fps*(NanoTime - time)/ 1000000000.0;
            time = NanoTime;
            
            while (delta >= 1) {
                clearGUI();
                drawSelection();
                drawResources();
                delta--;
            }
        }

        private void drawResources() {
            int lumber = ResourceManager.lumber;
            int minerals = ResourceManager.minerals;

            GameObject text = new GameObject();
            text.transform.parent = HUD_Base.gameObject.transform;
            text.name = "Lumber and Minerals";
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = "Lumber: " + lumber + "\nMinerals: " + minerals + "\nPopulation: " 
                + ResourceManager.population + " / " + ResourceManager.maxPopulation
                + "\nWave: " + ResourceManager.wave;
            textComponent.font = (Font)Resources.Load("UIFONT");
            textComponent.fontSize = 16;
            textComponent.color = Color.black;
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(144, -132, 0);
            rectTransform.sizeDelta = new Vector2(250, 200);
            guiElements.Add(text);

        }

        private void drawSelection() {
            if (selection.Count > 0) {
                LSAgent[] agents = selection.ToArray();
                selectionUnits = new Dictionary<string, FastList<LSAgent>>();

                foreach (LSAgent ag in agents) {
                    if (ag == null) {
                        continue;
                    }

                    if (!selectionUnits.ContainsKey(ag.gameObject.name)) {
                        selectionUnits.Add(ag.gameObject.name, new FastList<LSAgent>());   
                    }

                    selectionUnits[ag.gameObject.name].Add(ag);
                }

                string[] keys = new string[selectionUnits.Count];
                selectionUnits.Keys.CopyTo(keys, 0);


                //profileImage.GetComponent<Image>().sprite = getImage(keys[0].ToLower());
                int x = 0, y = 0;
                for (int i = 0; i < keys.Length; i++) {
                    drawUnitProfileImage(keys[i], selectionUnits[keys[i]].Count, x, y);
                    x += (int)(IMAGE_SIZE * 2);
                }
            } else {
                //profileImage.GetComponent<Image>().sprite = getImage("empty");

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
            try {
                return GameObject.Find(name).GetComponent<HealthVisual>().icon;
            } catch (NullReferenceException e) {
                name = name.ToLower();
                if (name.StartsWith("soldier")) {
                    return fighterImage;
                }

                if (name.StartsWith("town")) {
                    return townHallImage;
                }

                if (name.StartsWith("mineralminei")) {
                    return mineralImage;
                }

                if (name.StartsWith("lumbermill1")) {
                    return lumberImage;
                }
                UnityEngine.Debug.Log("Unable to find image for " + name);
                return emptyImage;
            }
              
        }

    }
}