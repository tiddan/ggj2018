using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleNPC : MonoBehaviour {


    public enum AIState {
        NeutralWalking,
        NeutralWaiting,
        Walking,
        Attacking,
        Waiting,
        Aggressive
    }
    public AIState currentState = AIState.NeutralWalking;


    public enum Influencer {
        None,
        RED,
        BLU
    }

    public Influencer currentInfluence = Influencer.None;


    NavMeshAgent thisAgent;

    // Does the NavAgent have a target?
    bool hasTarget = false;
    public float maxSpeed = 10.0f;

    public float blueInfluence = 0.0f;
    public float redInfluence = 0.0f;
    public float randomMaxRadius = 10.0f;
    public float influenceDecayRate = 0.1f;

    public float waitTime = 3.0f;
    float timeLeft = 0.0f;
    
	void Awake () {
        thisAgent = this.GetComponent<NavMeshAgent>();
	}
	
	void Update () {
	    
        switch (currentState) {
            case AIState.NeutralWaiting:
                HandleNeutralWaitingState();
                break;
            case AIState.NeutralWalking:
                HandleNeutralWalkingState();
                break;
            case AIState.Walking:
                HandleWalkingState();
                break;
            case AIState.Attacking:
                HandleAttackingState();
                break;
            case AIState.Waiting:
                HandleWaitingState();
                break;
            case AIState.Aggressive:
                HandleAggressiveState();
                break;
            default:
                break;
        }

        if (redInfluence > 0) {
            redInfluence -= influenceDecayRate * Time.deltaTime;
        } else if (redInfluence < 0) {
            redInfluence = 0;
        }
        if (blueInfluence > 0) {
            blueInfluence -= influenceDecayRate * Time.deltaTime;
        } else if (blueInfluence < 0) {
            blueInfluence = 0;
        }
        UpdateInfluencer();
    }




    #region StateMethods

    void HandleNeutralWalkingState () {
        if (hasTarget == false) {
            Vector3 randomDirection = Random.insideUnitSphere * randomMaxRadius;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, randomMaxRadius, 1);
            Vector3 finalPosition = hit.position;
            thisAgent.SetDestination(finalPosition);

            hasTarget = true;
        }
        if (thisAgent.hasPath && thisAgent.remainingDistance < 0.1f) {
            timeLeft = waitTime;
            currentState = AIState.NeutralWaiting;
        }
    }

    void HandleNeutralWaitingState () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            currentState = AIState.NeutralWalking;
            hasTarget = false;
        }
    }

    void HandleWalkingState () {

    }

    void HandleAttackingState () {

    }

    void HandleWaitingState () {

    }

    void HandleAggressiveState () {

    }


    #endregion


    public void Influence(string faction, float value) {
        
        switch (faction) {
            case ("BLU"):
                blueInfluence += value;
                break;
            case ("RED"):
                redInfluence += value;
                break;
            default:
                break;
        }

        UpdateInfluencer();
        
    }

    public void UpdateInfluencer() {
        switch (currentInfluence) {
            case (Influencer.None):
                if (blueInfluence > 1.0f && blueInfluence > redInfluence) {
                    currentInfluence = Influencer.BLU;
                    UpdateColor(Color.blue);
                } else if (redInfluence > 1.0f) {
                    currentInfluence = Influencer.RED;
                    UpdateColor(Color.red);
                }
                break;
            case (Influencer.BLU):
                if (redInfluence > blueInfluence) {
                    currentInfluence = Influencer.RED;
                    UpdateColor(Color.red);
                } else if (blueInfluence < 1.0f) {
                    currentInfluence = Influencer.None;
                    UpdateColor(Color.gray);
                }
                break;
            case (Influencer.RED):
                if (blueInfluence > redInfluence) {
                    currentInfluence = Influencer.BLU;
                    UpdateColor(Color.blue);
                } else if (redInfluence < 1.0f) {
                    currentInfluence = Influencer.None;
                    UpdateColor(Color.gray);
                }
                break;
            default:
                break;
        }
    }

    void UpdateColor(Color c) {
        this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = c;
    }

    #region CommandCalls

    public void WalkTo(Vector3 targetPosition) {

    }

    public void AttackTarget(GameObject target) {

    }

    public void BlockArea(Vector3 targetPosition) {

    }

    #endregion



}
