using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSound : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Bomb()
    {
        audioSource.Play();
    }

}
