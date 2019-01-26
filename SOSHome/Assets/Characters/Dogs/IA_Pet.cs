using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Pet : AiLifeform
{
    // bool 
    // Start is called before the first frame update

    GameObject cart = null;
    GameObject owner = null;

    void Start()
    {
        
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
            transform.position = owner.transform.position;
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
