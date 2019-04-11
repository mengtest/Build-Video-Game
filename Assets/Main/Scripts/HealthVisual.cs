using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour {

    public Canvas healthCanvas;

    private Canvas newCanvas;
    private Lockstep.Health hp;
    private float headYPos, width;
    private GameObject healthBar;


    void Start () {
        hp = this.GetComponent<Lockstep.Health>();
        Lockstep.LSBody body = this.GetComponent<Lockstep.UnityLSBody>().InternalBody;

        headYPos = Lockstep.FixedMath.ToFloat(body.HeightPos) 
            + Lockstep.FixedMath.ToFloat(body.Height)*2;

        width = 2*Lockstep.FixedMath.ToFloat(this.GetComponent<Lockstep.UnityLSBody>().InternalBody.Radius);

        newCanvas = Instantiate(healthCanvas);
        newCanvas.GetComponent<RectTransform>().position = new Vector3(0, headYPos, 0);
        newCanvas.GetComponent<RectTransform>().rotation = Quaternion.LookRotation(-Camera.main.transform.forward);

        newCanvas.transform.localScale = new Vector3(width/100.0f, width / 100.0f, 1);
        healthBar = newCanvas.transform.GetChild(0).GetChild(0).gameObject;
        healthBar.transform.localScale = new Vector3(hp.HealthAmount/hp.MaxHealth, 1, 1);

    }
	
	
	void Update () {
        Lockstep.LSBody body = this.GetComponent<Lockstep.UnityLSBody>().InternalBody;
        headYPos = Lockstep.FixedMath.ToFloat(body.HeightPos)
           + Lockstep.FixedMath.ToFloat(body.Height) * 2;

        newCanvas.GetComponent<RectTransform>().position = new Vector3(transform.position.x, headYPos, transform.position.z);
        newCanvas.GetComponent<RectTransform>().rotation = Quaternion.LookRotation(-Camera.main.transform.forward);

        float health = Lockstep.FixedMath.ToFloat(hp.HealthAmount) / Lockstep.FixedMath.ToFloat(hp.MaxHealth);
        healthBar.transform.localScale = new Vector3(health, 1, 1);
        if (health < 0.66) {
            healthBar.GetComponent<Image>().color = Color.yellow;
        }
        if (health < 0.33) {
            healthBar.GetComponent<Image>().color = Color.red;

        }
    }

    public void Die() {
        newCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
