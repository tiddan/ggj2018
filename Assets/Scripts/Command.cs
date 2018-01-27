using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour {

    public enum CommandType {
        Walk,
        Attack,
        Guard,
    }

    public CommandType thisCommand = CommandType.Walk;
    
    Vector3 target;


}
