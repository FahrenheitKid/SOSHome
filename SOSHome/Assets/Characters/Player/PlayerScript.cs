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
    public GameObject cart2;

    private List<GameObject> allCarts = new List<GameObject>();

    void Start() {
        cam = Camera.main.transform;
        allCarts.Add(cart);
        allCarts.Add(cart2);
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
        bool cartsAreFull = false;
        
        if (allCarts[0].GetComponent<CartScript>().occupied && allCarts[1].GetComponent<CartScript>().occupied) {
            cartsAreFull = true;
            }
        
        if (body.transform.tag == "Pet" && !cartsAreFull) {

            GameObject chosen_cart;
            if (!allCarts[0].GetComponent<CartScript>().occupied)
            {
                chosen_cart = allCarts[0];
            }
            else {
                chosen_cart = allCarts[1];
            }

            chosen_cart.GetComponent<CartScript>().occupied = true;
            chosen_cart.GetComponent<CartScript>().dog = body.gameObject;

            body.transform.SetParent(transform);
            body.transform.GetComponent<BoxCollider>().enabled = false;
            body.transform.GetComponent<IA_Pet>().setCartReference(chosen_cart);
            //body.transform.position = cart.transform.position;
        }
    }
}
