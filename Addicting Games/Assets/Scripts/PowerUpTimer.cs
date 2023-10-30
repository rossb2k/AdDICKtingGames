using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
    public float powerUpDuration = 5.0f;

    void Start()
    {
        Destroy(gameObject, powerUpDuration);
    }
}

