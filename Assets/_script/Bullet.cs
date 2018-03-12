using System.Collections;
using UnityEngine;

namespace BSU
{
    /// <summary>
    /// Описание поведения пули
    /// </summary>
    public class Bullet: MonoBehaviour
    {
        private GameObject _target;
        private float _speed;
        private float _damage;
        private Vector3 _spawnPosition;
        private Coroutine _shootCoroutine;

        // выстрел
        public void Shoot(GameObject target, Vector3 spawnposition, float speed,  float damage)
        {
            _target = target;
            _spawnPosition = spawnposition;
            _speed = speed;
            _damage = damage;

            gameObject.SetActive(true);
            _shootCoroutine = StartCoroutine(Shooting());
        }

        // сопрограмма для пермещение снаряда к цели
        IEnumerator Shooting()
        {
            while (Vector3.Distance(_target.transform.position, transform.position) > 0.2f)
            {
                if (!_target.activeInHierarchy)
                {
                    break;
                }
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                    Time.deltaTime * _speed);
           
                yield return null;
            }
            ForcedOff();
        }

        // принудительное выключение объекта
        public void ForcedOff()
        {
            StopCoroutine(_shootCoroutine);
            gameObject.SetActive(false);
        }
        
        // возвращает текущий урон снаряда
        public float GetDamage()
        {
            return _damage;
        }
        
        // Системное событие Unity. Выключение объекта в Hierarchy
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