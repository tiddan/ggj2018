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
    Animator animator;
    private Color blue, red;
    public MeshRenderer renderer;

    // Does the NavAgent have a target?
    bool hasTarget = false;
    public float maxSpeed = 10.0f;

    public float blueInfluence = 0.0f;
    public float redInfluence = 0.0f;
    public float randomMaxRadius = 10.0f;
    public float influenceDecayRate = 0.1f;

    public float waitTime = 3.0f;
    float timeLeft = 0.0f;


    GameObject attackTarget;
    bool isAttackingTarget = false;
    bool hasAttackPath = false;

	void Awake () {
        thisAgent = this.GetComponent<NavMeshAgent>();
	    animator = this.GetComponent<Animator>();
        //blue = new Color(0.3f,0.3f,1.0f);
        //red = new Color(1.0f,0.3f,0.3f);
	    blue = Color.blue;
	    red = Color.red;
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
        if (UpdateInfluencer()) {
            if (currentInfluence == Influencer.None) {
                currentState = AIState.NeutralWaiting;
            }
        }
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
            animator.SetTrigger("GoIdle");
        }
    }

    void HandleNeutralWaitingState () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            currentState = AIState.NeutralWalking;
            hasTarget = false;
            animator.SetTrigger("GoWalk");
        }
    }

    void HandleWalkingState () {
        if (thisAgent.hasPath && thisAgent.remainingDistance < 0.1f) {
            timeLeft = waitTime;
            currentState = AIState.Waiting;
            animator.SetTrigger("GoIdle");
        }
    }

    void HandleAttackingState () {
        if (attackTarget != null && !isAttackingTarget) {
            if (Vector3.Distance(this.transform.position, attackTarget.transform.position) < 1.0f) {
                StartAttack();
            } else if (!hasAttackPath) {
                NavMeshHit hit;
                NavMesh.SamplePosition(attackTarget.transform.position, out hit, 1.0f, 1);
                Vector3 finalPosition = hit.position;
                thisAgent.SetDestination(finalPosition);
            } else if (hasAttackPath && thisAgent.remainingDistance < 1.0f) {
                hasAttackPath = false;
            }
        } else if (isAttackingTarget) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {

            }
        }
    }

    void HandleWaitingState () {

    }

    void HandleAggressiveState () {

    }


    #endregion


    public void Influence(string faction, float value, string towerCommand, GameObject towerTarget) {
        
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

        if (UpdateInfluencer()) {
            switch (towerCommand) {
                case "Walk":
                    WalkTo(towerTarget.transform.position);
                    break;
                case "Attack":
                    AttackTarget(towerTarget);
                    break;
                default:
                    break;
            }
        }
        
    }

    public bool UpdateInfluencer() {
        switch (currentInfluence) {
            case (Influencer.None):
                if (blueInfluence > 1.0f && blueInfluence > redInfluence) {
                    currentInfluence = Influencer.BLU;
                    UpdateColor(blue);
                    UIController.instance.UpdateBluCrowd(1);
                    return true;
                } else if (redInfluence > 1.0f) {
                    currentInfluence = Influencer.RED;
                    UpdateColor(red);
                    UIController.instance.UpdateRedCrowd(1);
                    return true;
                }
                return false;
            case (Influencer.BLU):
                if (redInfluence > blueInfluence) {
                    currentInfluence = Influencer.RED;
                    UpdateColor(red);
                    UIController.instance.UpdateBluCrowd(-1);
                    UIController.instance.UpdateRedCrowd(1);
                    return true;
                } else if (blueInfluence < 1.0f) {
                    currentInfluence = Influencer.None;
                    UpdateColor(Color.white);
                    UIController.instance.UpdateBluCrowd(-1);
                    return true;
                }
                return false;
            case (Influencer.RED):
                if (blueInfluence > redInfluence) {
                    currentInfluence = Influencer.BLU;
                    UpdateColor(blue);
                    UIController.instance.UpdateBluCrowd(1);
                    UIController.instance.UpdateRedCrowd(-1);
                    return true;
                } else if (redInfluence < 1.0f) {
                    currentInfluence = Influencer.None;
                    UpdateColor(Color.white);
                    UIController.instance.UpdateRedCrowd(-1);
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    void UpdateColor(Color c) {
        if (c != blue && c != red)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.material.color = c;
            renderer.material.SetColor("_Emission",c);
            renderer.enabled = true;
        }
    }

    #region CommandCalls

    public void WalkTo(Vector3 targetPosition) {
        NavMeshHit hit;
        NavMesh.SamplePosition(targetPosition, out hit, 1.0f, 1);
        Vector3 finalPosition = hit.position;
        thisAgent.SetDestination(finalPosition);
    }

    public void AttackTarget(GameObject target) {
        attackTarget = target;
        currentState = AIState.Attacking;
    }

    public void DefendArea(Vector3 targetPosition) {

    }

    #endregion

    void StartAttack() {
        thisAgent.isStopped = true;
        animator.SetTrigger("GoAttack1");
        isAttackingTarget = true;
    }



}
