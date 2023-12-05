using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGettingBigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += (Time.deltaTime * 20.0f * Vector3.one);
        if (transform.localScale.x > 10.0f)
            Destroy(gameObject);
    }
}
