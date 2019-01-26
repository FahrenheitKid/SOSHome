using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variables used to control character speed of movement.
    float velocity = 6.0f;
    float runSpeed = 9.0f;
    float turnSpeed = 90;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

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

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

    }

    void CalculateDirection() {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;

        angle += cam.eulerAngles.y;

    }

    void Rotate() {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

    }

    void Move() {
        transform.position += transform.forward * velocity * Time.deltaTime;

    }
}
