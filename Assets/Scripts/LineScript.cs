using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{

    public Vector3 Origin;
    public Vector3 Destination;

    LineRenderer lineRenderer;
    private bool isActive;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (isActive)
	    {
	        lineRenderer.material.mainTextureOffset = new Vector2(-Time.timeSinceLevelLoad*10.0f, 0);
	    }
	}

    public void Configure(Vector3 origin, Vector3 destination, string team)
    {
        //var d = Vector3.Distance(Origin, Destination);

        Origin = origin;
        Destination = destination;
        lineRenderer.SetPositions(new [] { Origin, new Vector3(Destination.x, Destination.y+6.7f,Destination.z), });
        lineRenderer.material.mainTextureScale = new Vector2(50.0f,1.0f);
        //lineRenderer.material.color = team == "RED" ? Color.red : Color.blue;
        lineRenderer.material.SetColor("_TintColor", team == "RED" ? Color.red : Color.blue);
        
        isActive = true;
    }
}
