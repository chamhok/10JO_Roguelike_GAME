using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private GameObject square;
    private void Awake()
    {
    }
    void Start()
    {
        square = Instantiate((GameObject)Resources.Load("Square"));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)); 
        transform.RotateAround(center, Vector3.up, 10f * Time.deltaTime);
    }
}
