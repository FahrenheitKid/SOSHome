using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLifeform : MonoBehaviour
{
    public string type;

    private float velocity = 6.0f;
    public float rangeDelim = 40.0f;

    void Initialize() {
        //Better to write your own code to control starting variables.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomMovementPosition() {
        //This function is meant to return a Vector3 that is a point inside a range delimited by the rangeDelim variable.
        //That point is then used to instantiate (or move in case it has already been instantiated) a trigger to tell if
        //that point already has a solid structure. If it DOESNT have, set that point as a destination in a NavMesh agent.
        //Never change the Y position!
        Vector3 finalDestination = new Vector3(0, transform.position.y, 0);
        float n_z = Random.Range(transform.position.z, rangeDelim);
        float n_x = Random.Range(transform.position.x, rangeDelim);
        

        return finalDestination;
    }

}
