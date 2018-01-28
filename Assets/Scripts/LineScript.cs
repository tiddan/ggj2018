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
	        lineRenderer.material.mainTextureOffset = new Vector2(Time.timeSinceLevelLoad, 0);
	    }
	}

    public void Configure(Vector3 origin, Vector3 destination, string team)
    {
        var d = Vector3.Distance(Origin, Destination);

        Origin = origin;
        Destination = destination;
        lineRenderer.SetPositions(new [] { Origin, Destination});
        lineRenderer.material.mainTextureScale = new Vector2(d,1.0f);
        Vector3.Distance(Origin, Destination);
        isActive = true;
    }
}
