using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variables used to control character speed of movement.
    float velocity = 6.0f;
    //float runSpeed = 9.0f;
    //float turnSpeed = 90;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

    public GameObject cart;

    void Start() {
        cam = Camera.main.transform;
    }

    void Update() {
        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
        Rotate();
        Move();

    }

    void GetInput() {
        //Used to get the Input from the player;
        //Works with: Horizontal(a, d, Left, Right) and Vertical (w,s, Up, Down)
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

    }

    void CalculateDirection() {
        //Used to calculate the angle relative to the camera's rotation
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;

        angle += cam.eulerAngles.y;
    }

    void Rotate() {
        //Rotate towards the calculated angle
        targetRotation = Quaternion.Euler(0, angle, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move() {
        //Moves towards a Vector3.forward, relative to the target rotation!
        //transform.position += transform.forward * velocity * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetRotation, velocity * Time.deltaTime);
        Vector3 dir = targetRotation * Vector3.forward;
        transform.position += dir * velocity * Time.deltaTime;

    }

   

    void OnCollisionEnter(Collision body)
    {
        //print("Hey");
        if (body.transform.tag == "Pet") {
            body.transform.SetParent(cart.transform);
            body.transform.GetComponent<BoxCollider>().enabled = false;
            body.transform.position = cart.transform.position;
        }
    }
}
