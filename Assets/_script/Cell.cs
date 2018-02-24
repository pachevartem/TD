using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ArtelVR
{

    public class Cell : MonoBehaviour
    {

        #region publicVariables
        
        [Header("Перетяни кнопочки управляющие ячейкой")]
        public GameObject UI;
        
        [Header("Слой для врагов.")]
        public LayerMask Enemy;

        [Header("Место появления пули")]
        public Transform SpawnBolt;

        public SelectTarget aim = _ => { };
        
        #endregion

        #region privateVariables

        private TowerExample _typeTower;
        private List<Transform> Towers = new List<Transform>();
        
        private int _numberTower = 0;
        private float MydeltaTime= 0;
       
        
        #endregion

        #region Properties

        public int NumberTower
        {
            get { return _numberTower; }
            set
            {
                if (value >= Towers.Count)
                {
                    Helper.SetActive(UI, false);
                    return;
                }
                Helper.SetActive(Towers[_numberTower], false);
                _numberTower = value;
                Helper.SetActive(Towers[_numberTower], true);
            }
        }
        #endregion
        

        // 1 Создать башнии в ячейке 
        private void CreateTowerInCell(TowerExample typeTower)
        {
            for (int i = 0; i < typeTower.Levels.Count; i++)
            {
                var tower = Instantiate(typeTower.Levels[i], transform.position, Quaternion.identity, transform);
                Towers.Add(tower.transform);
                if (i != 0)
                {
                    tower.SetActive(false);
                }
            }
        }
        
        // Настроить ячейку.
        public void SetupCell(TowerExample typeTower)
        {
            _typeTower = typeTower;
            Helper.OffCollider(gameObject); // 2 Убрать возможность нажимать на ячеку
            Helper.SetActive(UI, true);  //Активность кнопки Upgrade
            CreateTowerInCell(typeTower);
//            StartCoroutine(UpdateCell()); // update ячейки
            StartCoroutine(refUpdateCell());
        }
        

        // Повысить уровень башни 
        public void LevelUp()
        {
            NumberTower++;
        }

        // Отрисовка в Editor радиуса поражения
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (!_typeTower)
            {
                return;
            }
            Gizmos.DrawWireSphere(transform.position, _typeTower.Raduis);
        }

        private Queue<GameObject> EnemysQueue  = new Queue<GameObject>(); //TODO: Перенести
        private bool _isDetected;
        
        [SerializeField]
        private Transform targetEnemy;

        private Coroutine Fire;

        void TimerOn()
        {
            MydeltaTime += Time.deltaTime;
        }

        void SortOutQueue(ref Queue<GameObject> queue)
        {
            while (!isVisible())
            {
                if (queue.Count==0)
                {
                   SetTarget(null);
                   break;
                }
                print("1");
                SetTarget(EnemysQueue.Dequeue());
            }
        }

        void SetTarget(GameObject target)
        {
            if (!target)
            {
                targetEnemy = null;
                return;
            }
            targetEnemy = target.transform; 
        }

        
        bool isVisible()
        {
            return Vector3.Distance(targetEnemy.position, transform.position) < _typeTower.Raduis;
        }
        
        IEnumerator refUpdateCell()
        {
            while (true)
            {   
                TimerOn();
                if (Physics.CheckSphere(transform.position, _typeTower.Raduis, Enemy))
                {
                    Helper.FillQueqe(Physics.OverlapSphere(transform.position, _typeTower.Raduis, Enemy), ref EnemysQueue);
                    aim(EnemysQueue.Peek());
                }
                else
                {
                    while (!isVisible())
                    {
                        SortOutQueue(ref EnemysQueue);
                        break;
                    }
                }
                yield return null;
            }
        }

        // Отдельный поток для стрельбы
        public IEnumerator UpdateCell()
        {
            while (true)
            {
                TimerOn();
                
                if (!_isDetected)
                {
                    print("Scaning - " + _isDetected);
                    var scanObjs = Physics.OverlapSphere(transform.position, _typeTower.Raduis, Enemy);
                    
                    if (scanObjs.Length>0)
                    {
                        Helper.FillQueqe(scanObjs,ref EnemysQueue);
                        _isDetected = true;
                        aim(EnemysQueue.Peek());
                        
                        if (isVisible())
                        {
                            yield return null;
                        }
                        else
                        {
                            _isDetected = false;
                            EnemysQueue.Clear();  
                        }
                        
                        if (Helper.ShootDelay(1, MydeltaTime) && targetEnemy)
                        {
                            print("St");
                            StartCoroutine(BulletFly(targetEnemy));
                            MydeltaTime = 0;
                        }
                    }
                    SetTarget(null);
                    _isDetected = false;
                }
                else
                {
                    continue;
                }
                
                yield return null;
            }
        }

        IEnumerator BulletFly(Transform target)
        {
            var a = Instantiate(_typeTower.Bolt, SpawnBolt.position, Quaternion.identity);
            while (Vector3.Distance(a.transform.position, target.position) >0.2f )
            {
                a.transform.position = Vector3.Slerp(a.transform.position,target.position, Time.deltaTime*5);
                yield return null;
            }
            Destroy(a.gameObject);    
        }
        
        void Awake()
        {
            Helper.SetActive(UI, false);
            aim += SetTarget;
        }
    }
}