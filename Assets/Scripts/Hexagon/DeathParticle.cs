using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class DeathParticle : MonoBehaviour, IPoolable
{
    public ParticleSystem particleSystem;
    public Color currentHexColor;

    public void OnDespawn()
    {

    }

    public void OnSpawn()
    {

    }

    public void StartParticle()
    {
        particleSystem.startColor = currentHexColor;
        particleSystem.Play();
    }

}
