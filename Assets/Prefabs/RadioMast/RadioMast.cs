using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMast : MonoBehaviour {

    public float lifeTime = 10.0f;
    public float signalStrength = 10.0f;
    public float signalRadius = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f) {
            DestroyMast();
        }
	}

    public void SetParams(float _lifeTime, float _signalStrength, float _signalRadius) {
        lifeTime = _lifeTime;
        signalStrength = _signalStrength;
        signalRadius = _signalStrength;
    }

    public void DestroyMast () {
        if (this.transform.parent != null) {
            this.transform.parent.GetComponent<Building>().FreeRoof();
        }
        
        Destroy(gameObject);
    }


}
