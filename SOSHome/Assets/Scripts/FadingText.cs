using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Timers;

public class FadingText : MonoBehaviour
{

    public TextMeshPro text3d;
    public float speed = 0.5f;
    public float lifetime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(selfDestruct());

        TimersManager.SetTimer(this, lifetime, changeAlpha);
  
    }

    // Update is called once per frame
    void Update()
    {

        
        transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * speed),transform.position.z);
        print(transform.position);
        if(TimersManager.RemainingTime(changeAlpha) > 0)
        {
            text3d.color = new Color(text3d.color.r, text3d.color.g, text3d.color.b, 1 - (lifetime - TimersManager.RemainingTime(changeAlpha)) / lifetime);
        }
       // text3d.color.a = 1;

    }

    void changeAlpha()
    {
        //TimersManager.RemainingTime(changeAlpha) / lifetime
       // text3d.color = new Color(text3d.color.r, text3d.color.g, text3d.color.b, 0.2f);
    }

    IEnumerator selfDestruct()
    {

        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
