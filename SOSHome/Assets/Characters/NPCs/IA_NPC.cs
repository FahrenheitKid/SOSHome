using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_NPC : AiLifeform
{
    public GameObject animator_object;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToChangeState;
        StartCoroutine(CountDownTimer(timeToChangeState));
        destination = GetRandomMovementPosition();
        //type = "Default";
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

    IEnumerator CountDownTimer(int _time) {
        //Use this coroutine as a timer function.
        yield return new WaitForSeconds(_time);
        
        ChangeState();
    }
    void MovingBehaviour() {
        //Uses GetRandomMovementPosition to get a random point around the entity, checks if it is an empty space, moves towards it using NavMesh.
        GetRandomMovementPosition();
    }

    void ChangeState() {
        //print("Change state!");
        if (current_state == States.IDLE) {
            destination = GetRandomMovementPosition();
            current_state = States.MOVING;
        }
        else if (current_state == States.MOVING) {
            current_state = States.IDLE;
        }
    }
}
