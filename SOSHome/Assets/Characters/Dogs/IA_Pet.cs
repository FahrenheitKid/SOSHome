using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Pet : AiLifeform
{
   // bool 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PutSpriteOnPlace();
    }

    void PutSpriteOnPlace() {
        if (transform.childCount > 0) {
            Transform sprite = transform.GetChild(0);
            sprite.localPosition = transform.position;
        }
    }
}
