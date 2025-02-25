using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFountainScript : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Invoke("StopWater", 1f);
        }
        else
        {
            Debug.LogError("No ParticleSystem component found.");
        }
    }

    void StopWater()
    {
        var emission = particleSystem.emission;
        emission.enabled = false;
    }
}
