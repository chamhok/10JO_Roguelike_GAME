using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
        public GameObject AttackPrefab;
        Vector3 _lookDir;
        Animator anim;
        GameObject _muzzle;

        private void Awake()
        {
                _muzzle = new GameObject("Muzzle");
                _muzzle.transform.parent = transform;
        }
        private void RotateMuzzle()
        {
                Vector3 PlayerPosition = GameManager.Instance.player.transform.position;
                _lookDir = PlayerPosition - transform.position;
                _lookDir.z = 0;
                _lookDir.Normalize();
                _muzzle.transform.rotation = Quaternion.Euler(_lookDir);

                float rotZ = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;

                //armRenderer.flipY = Mathf.Abs(rotZ) > 90f;        
                _muzzle.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        void SpawnPrefab()
        {
                var go = Instantiate(AttackPrefab, _muzzle.transform.position, _muzzle.transform.rotation);
                go.GetComponent<Projectile>()?.SetForward(_lookDir);
        }

        // Update is called once per frame
        void Update()
        {
                RotateMuzzle();
        }
}
