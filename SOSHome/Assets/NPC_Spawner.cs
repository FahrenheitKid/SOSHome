using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    public GameObject[] citz;
    public float timerspawn;
    public GameObject[] targt;
    public int rand, rand_char;
    public float spawntime;
    
    void Start()
    {
        spawntime = 0.2f;
    }
        
    void Update()
    {
        timerspawn += Time.deltaTime;
        if(timerspawn >= spawntime)
        {
            spawntime = Random.Range(0.6f, 2.3f);
            rand_char = Random.Range(0, citz.Length);
            rand = Random.Range(0, targt.Length);
            GameObject c = Instantiate(citz[rand_char], transform.position, transform.rotation);
            c.GetComponent<IA_CITIZEN>().target = targt[rand];
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        GameObject c = Instantiate(citz[rand_char], transform.position, transform.rotation);
        c.GetComponent<IA_CITIZEN>().target = targt[rand];
        timerspawn = 0;
    }
}
