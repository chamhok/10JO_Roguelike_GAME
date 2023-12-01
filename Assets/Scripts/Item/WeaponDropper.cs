using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropper : MonoBehaviour
{
    public string weaponName;
    GameObject _weaponPrefab;

    private void Awake()
    {
        string path = $"Item/{weaponName}";
        _weaponPrefab = Resources.Load<GameObject>(path);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(DestroyOneFrameLater());
    }

    IEnumerator DestroyOneFrameLater()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DropWeapon();
    }

    private void DropWeapon()
    {
        Instantiate(_weaponPrefab, transform.position, transform.rotation);
    }
}
