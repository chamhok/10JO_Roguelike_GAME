using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spreader : MonoBehaviour
{
    [SerializeField] Vector3 _offset = Vector3.right;
    [SerializeField] float _fireDelay = 1.0f;

    Vector3 _lookDir;
    GameObject _muzzle;

    bool _bFire;
    public GameObject ProjectilePrefab
    {
        get;
        private set;
    }

    public string ProjectilePrefabName
    {
        private get;
        set;
    }

    public float FireRate
    {
        set { _fireDelay = 1 / value; }
    }

    public Vector3 Offset
    {
        set { _offset = value; }
    }

    public float ArmLength
    {
        private get;
        set;
    }

    public float Power
    {
        private get;
        set;
    }


    private void Awake()
    {
        _muzzle = new GameObject("Muzzle");
        _muzzle.transform.parent = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _muzzle.transform.localPosition = _offset * ArmLength;
        float rotZ = Mathf.Atan2(_offset.y, _offset.x) * Mathf.Rad2Deg;
        _muzzle.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void LoadPrefab()
    {
        string path = "Item/" + ProjectilePrefabName;
        ProjectilePrefab = Resources.Load<GameObject>(path);
    }

    // Update is called once per frame
    void Update()
    {
        RotateMuzzle();
    }

    private void RotateMuzzle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDir = mousePosition - transform.position;
        _lookDir.z = 0;
        _lookDir.Normalize();        

        float rotZ = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;

        //armRenderer.flipY = Mathf.Abs(rotZ) > 90f;        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public void ThrowObject()
    {
        _bFire = true;
        LoadPrefab();

        StartCoroutine(IFire());
    }

    IEnumerator IFire()
    {
        while (_bFire)
        {
            yield return new WaitForSeconds(_fireDelay);
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        var rot = _muzzle.transform.rotation.eulerAngles + ProjectilePrefab.transform.rotation.eulerAngles;
        var go = Instantiate(ProjectilePrefab, _muzzle.transform.position, Quaternion.Euler(rot));
        go.GetComponent<Weapon>().Damage = Power;
        go.GetComponent<Projectile>()?.SetForward(_muzzle.transform.rotation * Vector2.right);
    }
}
