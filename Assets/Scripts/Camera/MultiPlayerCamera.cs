﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerCamera : MonoBehaviour
{
    private Animator cameraAnimator;

    public float maxRange = 1000f;

    public LayerMask hitMask;

    public GameObject BLUPointer, REDPointer;
    public GameObject BLUTarget, REDTarget;

    public GameObject radioMastPrefab;

    void Awake()
    {
        cameraAnimator = GetComponent<Animator>();
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        HandleInput();

        Ray BLUray = Camera.main.ScreenPointToRay(BLUPointer.GetComponent<RectTransform>().position);
        OnBLUHover(BLUray);

        Ray REDray = Camera.main.ScreenPointToRay(REDPointer.GetComponent<RectTransform>().position);
        OnREDHover(REDray);

        if (Input.GetMouseButtonUp(0) ) {
           // OnClick(ray);
        }

        if(Input.GetKeyDown("r")) {
            OnClick(BLUray, "BLU");
        }
        if (Input.GetKeyDown("e")) {
            SetBLUTarget(BLUray);
        }
        if(Input.GetKeyDown(KeyCode.RightControl)) {
            OnClick(REDray, "RED");
        }
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            SetREDTarget(REDray);
        }

        /* Camera change (4 fun) */
	    if (Input.GetKeyUp(KeyCode.F1))
	    {
	        //cameraAnimator.enabled = false;
	        cameraAnimator.SetTrigger("ShowCamera0");
	    }
	    if (Input.GetKeyUp(KeyCode.F2))
	    {
	        //cameraAnimator.enabled = true;
            cameraAnimator.SetTrigger("ShowCamera1");
	    }
	    if (Input.GetKeyUp(KeyCode.F3))
	    {
	        //cameraAnimator.enabled = true;
            cameraAnimator.SetTrigger("ShowCamera2");
	    }

    }

    void HandleInput() {


        //Player BLU
        if (Input.GetKey("a")) {
            BLUPointer.GetComponent<RectTransform>().transform.Translate(Vector3.left * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey("d")) {
            BLUPointer.GetComponent<RectTransform>().transform.Translate(Vector3.right * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey("w")) {
            BLUPointer.GetComponent<RectTransform>().transform.Translate(Vector3.up * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey("s")) {
            BLUPointer.GetComponent<RectTransform>().transform.Translate(Vector3.down * Time.deltaTime * 200.0f);
        }


        //Player RED
        if (Input.GetKey(KeyCode.LeftArrow)) {
            REDPointer.GetComponent<RectTransform>().transform.Translate(Vector3.left * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            REDPointer.GetComponent<RectTransform>().transform.Translate(Vector3.right * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            REDPointer.GetComponent<RectTransform>().transform.Translate(Vector3.up * Time.deltaTime * 200.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            REDPointer.GetComponent<RectTransform>().transform.Translate(Vector3.down * Time.deltaTime * 200.0f);
        }
    }



    public void OnBLUHover(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {
            Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.cyan);

            if (hit.transform.tag == "Building") {
                hit.transform.GetComponent<Building>().OnHover("BLU");
            }
        }
    }

    public void OnREDHover(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {
            Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.magenta);

            if (hit.transform.tag == "Building") {
                hit.transform.GetComponent<Building>().OnHover("RED");
            }
        }
    }

    public void OnClick (Ray ray, string player) {

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {
            Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.blue, 1.0f);

            if (hit.transform.tag == "Building") {
                switch (player) {
                    case "BLU":
                        hit.transform.GetComponent<Building>().OnClick(radioMastPrefab, player, "Attack", BLUTarget);
                        break;
                    case "RED":
                        hit.transform.GetComponent<Building>().OnClick(radioMastPrefab, player, "Attack", REDTarget);
                        break;
                    default:
                        break;
                }
                
            }
        }
    }

    public void SetBLUTarget (Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {

            if (hit.transform.tag == "Building") {
                BLUTarget = hit.transform.gameObject;
            }
        }
    }

    public void SetREDTarget (Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxRange, hitMask)) {

            if (hit.transform.tag == "Building") {
                REDTarget = hit.transform.gameObject;
            }
        }
    }
}
