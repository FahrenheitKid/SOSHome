﻿using UnityEngine;[RequireComponent(typeof(Light))]public class RedLightFlicker : MonoBehaviour{    private Light m_PointLight = null;    void Start()    {        m_PointLight = gameObject.GetComponent<Light>();    }    public void ChangeIntensity()    {        m_PointLight.intensity = Random.Range(0f, 3f);    }}