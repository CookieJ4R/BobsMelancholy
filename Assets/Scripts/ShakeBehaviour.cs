using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    private new Transform transform;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.1f;

    Vector3 initialPosition;

    void Awake()
    {
        if (transform == null)
            transform = GetComponent(typeof(Transform)) as Transform;
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float shakeDuration, float shakeMagnitude)
    {
        this.shakeMagnitude = shakeMagnitude;
        this.shakeDuration = shakeDuration;
    }

}
