using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Timers;


public class PlayerScript : MonoBehaviour
{
    //Variables used to control character speed of movement.
    public float velocity = 6.0f;
    //float runSpeed = 9.0f;
    //float turnSpeed = 90;

    Vector2 input;
    float angle;
    public GameObject animation_object;

    Quaternion targetRotation;
    Transform cam;

    public GameObject cart;
    public GameObject cart2;
    public GameObject firstJoint;
    public SpriteRenderer playerSprite;
    public Game game;

    private List<GameObject> allCarts = new List<GameObject>();

    private int dogCount = 0;
    private AudioManager audioManager;

    public GameObject[] wheel;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        cam = Camera.main.transform;
        allCarts.Add(cart);
        allCarts.Add(cart2);

       
    }

    public static void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
         //UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif

        SceneManager.LoadScene(0);
    }

    void Update() {
        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
        Rotate();
        Move();
        //firstJoint.transform.position = transform.position;

        
    }





    void GetInput() {
        
        //Used to get the Input from the player;
        //Works with: Horizontal(a, d, Left, Right) and Vertical (w,s, Up, Down)
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x > 0 || input.y > 0 || input.x < 0 || input.y < 0) {
            for(int k = 0; k < wheel.Length; k++)
            {
                wheel[k].GetComponent<Animator>().enabled = true;
                audioManager.PlayWheel();
            }
            
            animation_object.GetComponent<Animator>().SetBool("walk", true);
            if (input.x < 0) {
                animation_object.GetComponent<SpriteRenderer>().flipX = true;
                animation_object.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            else {
                animation_object.GetComponent<SpriteRenderer>().flipX = false;
                animation_object.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }
               
        }
        else
        {
            animation_object.GetComponent<Animator>().SetBool("walk", false);
            for (int k = 0; k < wheel.Length; k++)
            {
                wheel[k].GetComponent<Animator>().enabled = false;
                audioManager.StopWheel();
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
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
        Vector3 dir = targetRotation * Vector3.forward;
        //transform.position += dir * velocity * Time.deltaTime;
        GetComponent<CharacterController>().SimpleMove(dir * velocity * Time.deltaTime * 100);
        firstJoint.transform.position = transform.position;
    }

   

    void OnTriggerEnter(Collider body)
    {
        //This may be a little complicated to follow...
        bool cartsAreFull = false;
        //Search if any of the two carts are currently occupied.
        if (allCarts[0].GetComponent<CartScript>().occupied && allCarts[1].GetComponent<CartScript>().occupied) {
            cartsAreFull = true;
            }

        //If i am coliding with a pet....
        if (body.transform.tag == "Pet" && !cartsAreFull) {

            audioManager.PlayCaptureDog();

            GameObject chosen_cart;
            //Find what car is being occupied, with preference being the last one...
            if (!allCarts[0].GetComponent<CartScript>().occupied)
            {
                chosen_cart = allCarts[0];
            }
            else {
                chosen_cart = allCarts[1];
            }
            //When found, makes it be occupied, and set it to hold a reference to its respective dog.
            chosen_cart.GetComponent<CartScript>().occupied = true;
            chosen_cart.GetComponent<CartScript>().dog = body.gameObject;
            //Sets the parent of the dog to control movement, a reference to force it to stay with the cart, as well as disable the collider. It wont be needed anymore.
            body.transform.SetParent(transform);
            body.transform.GetComponent<BoxCollider>().enabled = false;
            body.transform.GetComponent<IA_Pet>().setCartReference(chosen_cart);

            Instantiate(game.starFX_prefab, new Vector3(body.transform.position.x, body.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.y * 1.2f, body.transform.position.z), Quaternion.identity);


        }
        else if (body.transform.tag == "NPC") {
            //If instead it collides with an NPC...
            //Gets the type that im supposed to check...
            string whatType = body.transform.GetComponent<IA_NPC>().type;
            //print(whatType);
            bool foundOwner = false;
            int cartIndex = 0;

            //Run through all the current carts to check if they have dogs on them.
            for (int i = 0; i < 2; i++) {
                if(allCarts[i].transform.GetComponent<CartScript>().dog != null)
                {//If they have...
                    if (allCarts[i].transform.GetComponent<CartScript>().dog.transform.GetComponent<IA_Pet>().type == whatType)
                    {//Compare the types. If its a match, stores the index of the cart that needs to be emptied, and triggers the next step...
                        foundOwner = true;
                        cartIndex = i;

          
                        FadingText go = Instantiate(game.fadingText_prefab, new Vector3 (transform.position.x, playerSprite.sprite.bounds.size.y, transform.position.z) , Quaternion.identity).GetComponent<FadingText>();
                        go.text3d.text = game.scorePoints(game.pointsPerDog).ToString();
                        audioManager.PlayCombo();
                        dogCount++;

                        if (dogCount == game.dogAmount)
                        {
                            game.score += (int)TimersManager.RemainingTime(game.printTimer);

                            go.text3d.text = game.score;
                            game.End();
                        }


                        Instantiate(game.heartFX_prefab, new Vector3(body.transform.position.x, body.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.y * 1.2f, body.transform.position.z), Quaternion.identity);


                    }
                }
            }

            if (foundOwner) {
                //Get the stored index of the correct cart, empties it (the bool of occupied and what dog instance), and sets the pet on to stay with its new owner.
                allCarts[cartIndex].transform.GetComponent<CartScript>().dog.transform.GetComponent<IA_Pet>().setOwnerReference(body.gameObject);
                allCarts[cartIndex].transform.GetComponent<CartScript>().occupied = false;
                allCarts[cartIndex].transform.GetComponent<CartScript>().dog = null;
            }
        }

    }
}
