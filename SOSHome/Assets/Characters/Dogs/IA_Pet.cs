using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Pet : AiLifeform
{
    // bool 
    // Start is called before the first frame update

    GameObject cart = null;
    GameObject owner = null;
    [SerializeField]private AnimatorOverrideController[] skin;
    private int randskin, randseed;
    public GameObject animation_object;

    void Start()
    {
        randseed = Random.Range(0, 150);
        Random.seed = randseed;
        randskin = Random.Range(0, skin.Length);
        Debug.Log(randskin);
        animation_object.GetComponent<Animator>().runtimeAnimatorController = skin[randskin];
    }

    // Update is called once per frame
    void Update()
    {
        StayOnCartPosition();
    }

    void StayOnCartPosition() {
        if (cart != null) {
            transform.position = cart.transform.position;
        }
        if (owner != null) {
            transform.position = owner.transform.position + new Vector3(-1,0,-0.5f);
        }
    }

    public void setCartReference(GameObject _cart) {
        cart = _cart;
    }

    public void setOwnerReference(GameObject _owner) {
        owner = _owner;
        cart = null;
    }
}
