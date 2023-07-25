using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalControl : MonoBehaviour
{
    private static ParticleSystem particle;

    private static ParticalControl instance;

    public static ParticalControl Instance { get => instance; }

    private void Start()
    { 
        instance = this;
        particle = GetComponent<ParticleSystem>();
        Player.OnPlayerFinish += play;
    }

    public void play()
    {
        particle.Play();
    }
}
