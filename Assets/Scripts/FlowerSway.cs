using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 11/14/25
    Date Last Updated: 11/14/25
    Summary: Makes flower sway in wind
*/

public class FlowerSway : MonoBehaviour
{
    public float swaySpeed = 1f; // how fast it sways
    public float swayAmount = 10f;// degrees of rotation

    private float startRotation;

    void Awake()
    {
        startRotation = transform.localEulerAngles.x;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localRotation = Quaternion.Euler(startRotation + sway, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
