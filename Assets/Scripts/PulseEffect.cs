using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed;
    private float maxSize;
    public float fadeSpeed;

    public float secondPulseStart;


    private void Awake()
    {
        pulseSpeed *= 0.01f;
    }
    private void OnEnable()
    {
        maxSize = transform.localScale.x > transform.localScale.z ? transform.localScale.x : transform.localScale.z;
    }
    private void Update()
    {
        transform.localScale += new Vector3(pulseSpeed, 0, pulseSpeed);

        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;

        if (objectColor.a < 0)
        {
            Destroy(this.gameObject);
        }

    }
}
