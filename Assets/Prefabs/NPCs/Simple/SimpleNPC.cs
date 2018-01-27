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


    NavMeshAgent thisAgent;

    // Does the NavAgent have a target?
    bool hasTarget = false;
    public float maxSpeed = 10.0f;

    public float blueInfluence = 0.0f;
    public float redInfluence = 0.0f;
    public float randomMaxRadius = 10.0f;

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

	}




    #region State methods

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

        
}
