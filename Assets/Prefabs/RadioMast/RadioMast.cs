using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMast : MonoBehaviour {

    public float lifeTime = 10.0f;
    public float signalStrength = 10.0f;
    public float signalRadius = 10.0f;
    public float signalFrequence = 1.0f;

    public string thisOwner = "RED";

    float pulseTimer = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f) {
            DestroyMast();
        }

        pulseTimer -= Time.deltaTime;

        if(pulseTimer <= 0) {
            Pulse();
            pulseTimer = signalFrequence;
        }
       

	}

    public void SetOwner(string owner)
    {
        thisOwner = owner;
        var ps = GetComponentInChildren<ParticleSystem>().main;
        ps.startColor = owner == "RED"
            ? new Color(1.0f, 0.25f, 0.25f)
            : new Color(0.25f, 0.25f, 1.0f);
    }

    //public void SetParams(string owner, float _lifeTime, float _signalStrength, float _signalRadius, float _pulseTimer) {
    //    thisOwner = owner;
    //    lifeTime = _lifeTime;
    //    signalStrength = _signalStrength;
    //    signalRadius = _signalStrength;
    //    pulseTimer = _pulseTimer;
    //}

    public void DestroyMast () {
        if (this.transform.parent != null) {
            this.transform.parent.GetComponent<Building>().FreeRoof();
        }
        
        Destroy(gameObject);
    }


    void Pulse () {

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.root.position, signalRadius);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].gameObject.tag == "NPC") {
                hitColliders[i].gameObject.GetComponent<SimpleNPC>().Influence(thisOwner, 1.0f);
            }
            i++;
        }

    }
    


}
