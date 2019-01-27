using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour
{

    public SpriteRenderer sprRenderer;

    public float depthPrecision;
    public float localLayer;

    public static Depth depthHandler;

    void Start()
    {
        depthHandler = this;
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.z) + (int)localLayer;
    }
}