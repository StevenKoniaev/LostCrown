using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier = 4f;
    private void Start()
    {
        instance = this;
    }
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {

       
        shakeTimeRemaining -= Time.deltaTime;
        float xAmount = Random.Range(-1f, 1f) * shakePower;
        float yAmount = Random.Range(-1f, 1f) * shakePower;

        transform.position += new Vector3(xAmount, yAmount, 0f);
        shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

        shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * Time.deltaTime * rotationMultiplier);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }
    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
        shakeFadeTime = power / length;
        shakeRotation = power * rotationMultiplier;
    }
}
