using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLifeform : MonoBehaviour
{
    public string type;

    public float velocity = 6.0f;
    protected Vector3 startingPosition = new Vector3(0,0,0);
    public float rangeDelim = 40.0f;

    protected enum States { IDLE, MOVING };

    protected States current_state = States.IDLE;

    void Initialize() {
        //Better to write your own code to control starting variables.
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Vector3 GetRandomMovementPosition() {
        //This function is meant to return a Vector3 that is a point inside a range delimited by the rangeDelim variable.
        //That point is then used to instantiate (or move in case it has already been instantiated) a trigger to tell if
        //that point already has a solid structure. If it DOESNT have, set that point as a destination in a NavMesh agent.
        //Never change the Y position!
        Vector3 finalDestination = new Vector3(0, transform.position.y, 0);

        //There must be a more organized way to do this but...I am tired.
        int n = Random.Range(0,1);
        float n_z = 0.0f;
        float n_x = 0.0f;

        if (n == 0)
            n_z = Random.Range(startingPosition.z, rangeDelim);
        
        else 
            n_z = Random.Range(startingPosition.z, -rangeDelim);
        
        n = Random.Range(0, 1);
        if (n == 0)
            n_x = Random.Range(startingPosition.x, rangeDelim);
        
        else
            n_x = Random.Range(startingPosition.x, -rangeDelim);

        finalDestination.z = n_z;
        finalDestination.x = n_x;

        return finalDestination;
    }
}
