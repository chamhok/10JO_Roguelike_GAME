using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Boomb : MonoBehaviour
{
    [SerializeField] float _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);        
    }
}
