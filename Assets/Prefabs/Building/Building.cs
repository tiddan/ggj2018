using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {


    public Transform RadioMastAnchorPoint;
    public Color originalColor;

    public bool isHovering = false;
    public bool hoverEnter = true;

    public bool hasRadioMastOn = false;

    public MeshRenderer houseRenderer;

    public void Awake() {
        originalColor = houseRenderer.material.color;
    }

    public void Update() {
        if (isHovering) {
            isHovering = false;
        } else {
            hoverEnter = true;
            houseRenderer.material.color = originalColor;
        }
    }

    public void OnClick (GameObject radioMast, string influencer) {

        /*
         * Debug
         */
        switch (influencer) {
            case "BLU":
                houseRenderer.material.color = Color.blue;
                break;
            case "RED":
                houseRenderer.material.color = Color.red;
                break;
            default:
                break;
        }
        

        if (!hasRadioMastOn) {
            GameObject RM = Instantiate(radioMast, RadioMastAnchorPoint.position, RadioMastAnchorPoint.rotation);
            RM.transform.SetParent(this.transform);
            RM.GetComponent<RadioMast>().SetOwner(influencer);
            hasRadioMastOn = true;
        }
       
    }

    public void OnHover (string influencer) {

        switch (influencer) {
            case ("BLU"):
                if (hoverEnter == true) {
                    houseRenderer.material.color = Color.cyan;
                    hoverEnter = false;
                }
                isHovering = true;
                break;
            case ("RED"):
                if (hoverEnter == true) {
                    houseRenderer.material.color = Color.magenta;
                    hoverEnter = false;
                }
                isHovering = true;
                break;
            default:
                if (hoverEnter == true) {
                    houseRenderer.material.color = Color.green;
                    hoverEnter = false;
                }
                isHovering = true;
                break;
        }
       
    }

    public void FreeRoof() {
        hasRadioMastOn = false;
    }

}
