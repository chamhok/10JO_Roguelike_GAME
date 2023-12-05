using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyObjectController : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5.0f);
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
