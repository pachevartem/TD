using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class CellController : MonoBehaviour
    {
        public GameObject UpgrateUI;


        private List<GameObject> _lvlModels = new List<GameObject>();
        private GameObject _bullet;
        private float _affectedArea;
        private float _damage;
        private float _fireDelay;
        private LayerMask _enemyLayer;
        private Vector3 _spawnBolt;


        private GameObject targetEnemy;

        //счетчик
        private float _dt = 0;

        private int _lvlTower = 0;
        
        //свойства 
        public int LvlTower
        {
            get { return _lvlTower; }
            set
            {
                print(value);
                
                if (value >= _lvlModels.Count-1)
                {
                    Helper.SetActive(UpgrateUI, false);
//                    return;
                }
                Helper.SetActive(_lvlModels[LvlTower], false);
                _lvlTower = value;
                Helper.SetActive(_lvlModels[LvlTower], true);
            }
        }

        //конструкор ячейки
        public  void ConstructorSell(TowerExample t)
        {
            _bullet = t.Bolt;
            _affectedArea = t.Raduis;
            _damage = t.CountHit;
            _fireDelay = t.FireDelay;
            _enemyLayer = t.EnemyLayer;
            _spawnBolt = transform.position + Vector3.up * 0.4f; // магическое число заменить на высоту башни
            SetTarget(null);
            CrTower(t);
            offColider();
            Helper.SetActive(UpgrateUI, true);
            subscription(); // обязательно в окнце конструктора
        }

        void CrTower(TowerExample t)
        {
            for (int i = 0; i < t.Levels.Count; i++)
            {
               _lvlModels.Add(Instantiate(t.Levels[i], transform.position, Quaternion.identity, transform));
                Helper.SetActive(_lvlModels[i], false);
            }
            Helper.SetActive(_lvlModels[0], true);
        }

        void offColider()
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        public void UpgrateTower()
        {
            LvlTower++;
        }

        void TimerOn()
        {
            _dt += Time.deltaTime;
        }

        void SetTarget(GameObject target)
        {
            targetEnemy = target;
        }

        private bool isDetected;
        private Queue<GameObject> Enemys = new Queue<GameObject>();
        void UpdateScan()
        {
            TimerOn();

            if (!ScanArea())
            {
                return;
            }
            
            if (ScanArea() && !isDetected)
            {
                isDetected = true;
                Helper.FillQueqe(Physics.OverlapSphere(transform.position,_affectedArea,_enemyLayer), ref Enemys);
                SetTarget(Enemys.Peek()); //TODO: eventFire changes here
                return;
            }
            
            if (!CheckArea())
            {
                while (!CheckArea())
                {
                    if (Enemys.Count ==0)
                    {
                        SetTarget(null);
                        isDetected = false;
                        return;
                    }
                    SetTarget(Enemys.Dequeue());
                }
            }

            if (Helper.ShootDelay(_fireDelay,_dt) && targetEnemy)
            {
                print("shoot");
                StartCoroutine(BulletFly(targetEnemy.transform));
                _dt = 0;
            }
            
        }

        IEnumerator BulletFly(Transform target)
        {
            var a = Instantiate(_bullet ,_spawnBolt, Quaternion.identity);
            while (Vector3.Distance(a.transform.position, target.position) >0.3f )
            {
                a.transform.position = Vector3.Slerp(a.transform.position,target.position, Time.deltaTime*5);
                yield return null;
            }
            Destroy(a.gameObject);    
        }
        
        
        bool CheckArea()
        {
            return Vector3.Distance(targetEnemy.transform.position, transform.position) < _affectedArea;
        }

        bool ScanArea()
        {
            return Physics.CheckSphere(transform.position, _affectedArea, _enemyLayer);
        }


        void subscription()
        {
            GameController.OnUpdate += UpdateScan;
        }


        private void Awake()
        {
            Helper.SetActive(UpgrateUI, false);
            
        }
    }
}