using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_CITIZEN : MonoBehaviour
{
    public GameObject target;
    public float dst_target;
    public GameObject animator_object, hat;
    private float speed;

    void Start()
    {
        animator_object.GetComponent<Animator>().SetBool("walk", true);
        speed = 3.8f;
    }

    void Update()
    {
        dst_target = Vector3.Distance(transform.position, target.transform.position);
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime );

        if(dst_target <= 1)
        {
            if(target.GetComponent<NPC_Point>().point_to_go != null && target.GetComponent<NPC_Point>().destroy == false)
                target = target.GetComponent<NPC_Point>().point_to_go;
            else if (target.GetComponent<NPC_Point>().destroy == true)
            {
                Destroy(gameObject);
            }

            if (transform.position.x > target.transform.position.x)
            {
                animator_object.GetComponent<SpriteRenderer>().flipX = true;
                hat.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator_object.GetComponent<SpriteRenderer>().flipX = false;
                hat.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
    }
}
