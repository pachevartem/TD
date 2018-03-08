using System.Collections;
using UnityEngine;

namespace ArtelVR.TestRun.Learn
{
    public class Bullet: MonoBehaviour
    {
        private GameObject _target;
        private float _speed;
        private float _damage;
        
        private Vector3 _spawnPosition;

        private Coroutine _shootCoroutine;
        

        public void Shoot(GameObject target, Vector3 spawnposition, float speed,  float damage)
        {
            _target = target;
            _spawnPosition = spawnposition;
            _speed = speed;
            _damage = damage;

            gameObject.SetActive(true);
            _shootCoroutine = StartCoroutine(Shooting());
        }


        IEnumerator Shooting()
        {
            while (Vector3.Distance(_target.transform.position, transform.position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                    Time.deltaTime * _speed);
           
                yield return null;
            }
            ForcedOff();
        }

        public void ForcedOff()
        {
            StopCoroutine(_shootCoroutine);
            gameObject.SetActive(false);
        }

        public float GetDamage()
        {
            return _damage;
        }

        private void OnDisable()
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine); 
            }
            transform.position = _spawnPosition;
        }

    }
}