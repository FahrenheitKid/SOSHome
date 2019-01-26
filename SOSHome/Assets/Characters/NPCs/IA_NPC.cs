using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_NPC : AiLifeform
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current_state == States.IDLE)
            IdleBehaviour();
        
        if (current_state == States.MOVING)
            MovingBehaviour();
    }

    void IdleBehaviour() {
        //Just stops for a couple of seconds.
    }

    void MovingBehaviour() {
        //Uses GetRandomMovementPosition to get a random point around the entity, checks if it is an empty space, moves towards it using NavMesh.
        GetRandomMovementPosition();
    }
}
