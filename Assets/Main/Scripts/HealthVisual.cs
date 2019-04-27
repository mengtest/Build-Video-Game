using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour {

    public Canvas healthCanvas;
    private GameObject minimap;
    public Sprite icon;

    private Canvas newCanvas;
    private Lockstep.Health hp;
    private float headYPos, width;
    private GameObject healthBar;
    private GameObject minimapIcon;



    void Start () {
        createHealthbar();
        createMinimapIcon();
    }

    void createMinimapIcon() {
        minimap = GameObject.Find("MinimapImage");
        minimapIcon = new GameObject("MinimapIcon");
        minimapIcon.layer = 5;
        Image i = minimapIcon.AddComponent<Image>();
        
        minimapIcon.transform.SetParent(minimap.transform);
        RectTransform rectTransform = minimapIcon.GetComponent<RectTransform>();
        if (GetComponent<Lockstep.Move>()) {//if unit is movable, aka not a building
            rectTransform.sizeDelta = new Vector2(3, 3);
        } else {
            rectTransform.sizeDelta = new Vector2(6, 6);
            rectTransform.transform.SetAsFirstSibling();
        }
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void drawMinimapIcon() {
        float minimapWidth = minimap.GetComponent<RectTransform>().sizeDelta.x;
        float minimapHeight = minimap.GetComponent<RectTransform>().sizeDelta.y;
        float newX = (minimapWidth/ 1024.0f) * (transform.position.x);
        float newY = (minimapHeight / 1024.0f) * (transform.position.z);

        minimapIcon.GetComponent<RectTransform>().transform.localPosition = new Vector3(newX, newY, 0);

        Lockstep.AllegianceType allegianceType = Lockstep.AllegianceType.All;

        try {
            allegianceType = Lockstep.PlayerManager.GetAllegiance(GetComponent<Lockstep.LSAgent>().Controller);
        } catch (NullReferenceException e) {
            Debug.Log(e.Message);
            return;
        }

        Color sideColor;
        switch (allegianceType) {
            case (Lockstep.AllegianceType.Friendly):
                sideColor = Color.blue;
                break;
            case (Lockstep.AllegianceType.Neutral):
                sideColor = Color.blue;
                break;
            case (Lockstep.AllegianceType.Enemy):
                minimapIcon.GetComponent<Transform>().SetAsLastSibling();
                sideColor = Color.red;
                break;
            default:
                sideColor = Color.white;
                break;
        }

        minimapIcon.GetComponent<Image>().color = sideColor;
    }

    void createHealthbar() {
        hp = this.GetComponent<Lockstep.Health>();
        Lockstep.LSBody body = this.GetComponent<Lockstep.UnityLSBody>().InternalBody;

        headYPos = Lockstep.FixedMath.ToFloat(body.HeightPos)
            + Lockstep.FixedMath.ToFloat(body.Height) * 2;

        width = 2 * Lockstep.FixedMath.ToFloat(this.GetComponent<Lockstep.UnityLSBody>().InternalBody.Radius);

        newCanvas = Instantiate(healthCanvas);
        newCanvas.GetComponent<RectTransform>().position = new Vector3(0, headYPos, 0);
        newCanvas.GetComponent<RectTransform>().rotation = Quaternion.LookRotation(-Camera.main.transform.forward);

        newCanvas.transform.localScale = new Vector3(width / 100.0f, width / 100.0f, 1);
        healthBar = newCanvas.transform.GetChild(0).GetChild(0).gameObject;
        healthBar.transform.localScale = new Vector3(hp.HealthAmount / hp.MaxHealth, 1, 1);
    }
	

    void drawHealthBar() {
        Lockstep.LSBody body = this.GetComponent<Lockstep.UnityLSBody>().InternalBody;
        headYPos = Lockstep.FixedMath.ToFloat(body.HeightPos)
           + Lockstep.FixedMath.ToFloat(body.Height) * 2;
        float backOfHead = transform.position.z + Lockstep.FixedMath.ToFloat(body.Radius);

        newCanvas.GetComponent<RectTransform>().position = new Vector3(transform.position.x, headYPos, backOfHead);
        newCanvas.GetComponent<RectTransform>().rotation = Quaternion.LookRotation(-Camera.main.transform.forward);

        float health = Lockstep.FixedMath.ToFloat(hp.HealthAmount) / Lockstep.FixedMath.ToFloat(hp.MaxHealth);
        healthBar.transform.localScale = new Vector3(health, 1, 1);

        Lockstep.AllegianceType allegianceType = Lockstep.AllegianceType.All;

        try {
            allegianceType = Lockstep.PlayerManager.GetAllegiance(GetComponent<Lockstep.LSAgent>().Controller);
        } catch (NullReferenceException e) {
            Debug.Log(e.Message);
            return;
        }

        Color hpColor;
        switch (allegianceType) {
            case (Lockstep.AllegianceType.Friendly):
                hpColor = Color.green;
                break;
            case (Lockstep.AllegianceType.Neutral):
                hpColor = Color.blue;
                break;
            case (Lockstep.AllegianceType.Enemy):
                hpColor = Color.red;
                break;
            default:
                hpColor = Color.white;
                break;
        }

        hpColor = Color.Lerp(hpColor, Color.black, 1 - health);
        healthBar.GetComponent<Image>().color = hpColor;

    }

    void Update () {
        drawHealthBar();
        drawMinimapIcon();
    }
    public void Die() {
        newCanvas.gameObject.SetActive(false);
        minimapIcon.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
