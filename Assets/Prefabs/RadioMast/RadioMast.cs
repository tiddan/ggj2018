using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class RadioMast : MonoBehaviour {

    public float lifeTime = 10.0f;
    public float signalStrength = 10.0f;
    public float signalRadius = 10.0f;
    public float signalFrequence = 1.0f;

    public string thisOwner = "RED";
    public Vector3 RedOrigin, BluOrigin;
    public GameObject LinePrefab;

    float pulseTimer = 0.0f;

    public string towerCommand;
    public GameObject towerTarget;

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

        var lr = GameObject.Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
        lr.transform.SetParent(this.transform);
        var ls = lr.GetComponent<LineScript>();
        ls.Configure(owner=="RED" ? RedOrigin : BluOrigin,transform.position,owner);
        transform.GetComponentInChildren<Light>().color = owner == "RED" ? Color.red : Color.blue;
    }

    public void SetCommand(string command, GameObject target) {
        towerCommand = command;
        towerTarget = target;
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

    s
    void Pulse () {

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.parent.position, signalRadius);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].gameObject.tag == "NPC") {
                hitColliders[i].gameObject.GetComponent<SimpleNPC>().Influence(thisOwner, 1.0f, towerCommand, towerTarget);
            }
            i++;
        }

    }
    

}
