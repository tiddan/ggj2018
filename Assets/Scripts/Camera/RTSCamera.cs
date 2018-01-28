using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {


    public float maxRange = 1000f;

    public LayerMask hitMask;



    public GameObject radioMastPrefab;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        OnHover(ray);

        if (Input.GetMouseButtonUp(0) ) {
            OnClick(ray);
        }

	}

    public void OnHover(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {
            Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.cyan);

            if (hit.transform.tag == "Building") {
                hit.transform.GetComponent<Building>().OnHover("RED");
            }
        }

        
    }

    public void OnClick (Ray ray) {

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {
            Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.blue, 1.0f);

            if (hit.transform.tag == "Building") {
                //hit.transform.GetComponent<Building>().OnClick(radioMastPrefab, "RED");
            }
        }
    }
}
