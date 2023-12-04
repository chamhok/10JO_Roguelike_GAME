using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Bomb : MonoBehaviour
{
    Weapon _weapon;

    public string weaponName;
    GameObject _prefabBomb;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _prefabBomb = Resources.Load<GameObject>($"Item/{weaponName}");
    }

    private void Start()
    {
        _prefabBomb.GetComponent<Weapon>().Damage = _weapon.Damage * 2.0f;
    }

    private void OnDestroy()
    {
        var go = Instantiate(_prefabBomb, transform.position, transform.rotation);
        go.GetComponent<Weapon>().Damage = _weapon.Damage * 2.0f;
    }
}
