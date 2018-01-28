using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{

    public Vector3 Center;
    public float Radius;
    public float Speed;
    public float Height;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3(Radius * Mathf.Sin(Time.timeSinceLevelLoad*Speed), Height, Radius * Mathf.Cos(Time.timeSinceLevelLoad*Speed));
        Camera.main.transform.LookAt(Center);
	}
}
