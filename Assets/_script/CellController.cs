using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class CellController : MonoBehaviour //TODO: Добавить жизни башни и переисовать интерфейс, а не то это пздц
    { 
        [Header("Перетащи кнопку для улучшения башни")]
        public GameObject UpgrateUI;

        #region Приватные Переменные

        //коллекция башень (уровень 1-N)
        private List<GameObject> _lvlModels = new List<GameObject>(); 
        
        // моедль снаряда
        private GameObject _bullet;        

        // радиус поражения
        private float _affectedArea;        

        // урон снаряда
        private float _damage; //TODO: перенести в пулю         

        // задержка стрельбы башни
        private float _fireDelay;         

        // слой с противниками
        private LayerMask _enemyLayer;
        
        // место появления снарада
        private Vector3 _spawnBolt;
        
        // стартовое количество в пуле снарядов
        private int _cBullet = 3;
        
        // цель, которую атакует башня
        private GameObject targetEnemy;
        
        // список пула снарядов
        private List<GameObject> _poolBullets = new List<GameObject>();
        
        // флаг обнаружения врагов
        private bool isDetected;
        
        // очередь врагов в зоне поражения башни
        private Queue<GameObject> Enemys = new Queue<GameObject>();
        
        //счетчик времени (используется для атаки башни)
        private float _dt = 0;
        
        // индификатор уровня башни (ниже есть свойство)
        private int _lvlTower = 0;

        //свойства 
        private int LvlTower
        {
            get { return _lvlTower; }
            set
            {
//                print(value);

                if (value >= _lvlModels.Count - 1)
                {
                    Helper.SetActive(UpgrateUI, false);
                }
                Helper.SetActive(_lvlModels[LvlTower], false);
                _lvlTower = value;
                Helper.SetActive(_lvlModels[LvlTower], true);
                UIController.Bank -= 100;
            }
        }
        #endregion
        
        //конструкор ячейки
        public void ConstructorSell(TowerExample t)
        {
            _bullet = t.Bolt;
            _affectedArea = t.Raduis;
            _damage = t.CountHit;
            _fireDelay = t.FireDelay;
            _enemyLayer = t.EnemyLayer;
            _spawnBolt = transform.position + Vector3.up * 0.4f; // магическое число заменить на высоту башни
           
            SetTarget(null); 
            CrTower(t); //построить башни
            OffColider(); //выключить взаимодействие с башней
            Helper.SetActive(UpgrateUI, true); // включить кнопку Upgrade
            CreatePoolBullets();  // сгенерировать стартовый надо пуль
            subscription(); // обязательно в окнце конструктора
            UIController.Bank -= 100;
        }

        //Создать башни
        void CrTower(TowerExample t)
        {
            for (int i = 0; i < t.Levels.Count; i++)
            {
                _lvlModels.Add(Instantiate(t.Levels[i], transform.position, Quaternion.identity, transform));
                Helper.SetActive(_lvlModels[i], false);
            }
            Helper.SetActive(_lvlModels[0], true);
        }

        // метод используются во внешнем интерфейсе через панел Inspector
        public void UpgrateTower()
        {
            LvlTower++;
        }
        
        //начать отсчет
        void TimerOn()
        {
            _dt += Time.deltaTime;
        }
        
        // установить цель для атаки
        void SetTarget(GameObject target)
        {
            targetEnemy = target;
        }
        
        
        // Сканирование местности
        void UpdateScan()
        {
            TimerOn();
            //если местность пуста
            if (!ScanArea())
            {
                return;
            }
            
            // если местность не пуста, но враги не обнаружены
            if (ScanArea() && !isDetected)
            {
                isDetected = true;
                Helper.FillQueqe(Physics.OverlapSphere(transform.position, _affectedArea, _enemyLayer), ref Enemys);
                SetTarget(Enemys.Peek()); //TODO: eventFire changes here
                return;
            }
            // если враги обнаружены, необзодимо проверить текущую цель на нахождение в заоне поражения
            if (!CheckArea() && targetEnemy)
            {
                while (!CheckArea()) // если цель вышла из зоны поражения, то найти следующий элемент в очереди, который находится в зоне поражения
                {
                    if (Enemys.Count == 0)
                    {
                        SetTarget(null);
                        isDetected = false;
                        return;
                    }
                    SetTarget(Enemys.Dequeue());
                }
            }
            // стрельба по цели
            if (Helper.ShootDelay(_fireDelay, _dt) && targetEnemy)
            {
                 StartCoroutine(BulletFly(targetEnemy.transform));
                _dt = 0;
            }

        }
        
        // проверка дистанции полета пули.
        private bool CheckRangeBullet(Transform b, Transform t)
        {
            return  Vector3.Distance(b.position, t.position) > 0.3f && Vector3.Distance(b.position, t.position) < _affectedArea;
        }
        
        //псевдопоток для полета пули до цели
        IEnumerator BulletFly(Transform target)
        {
            var a = GetBullet();
            while (CheckRangeBullet(a.transform, target))
            {
                a.transform.position = Vector3.Slerp(a.transform.position, target.position, Time.deltaTime * 5);
                yield return null;
            }
            a.GetComponent<BulletItem>().Off();
        }
        
        // сгенерировать пул объектов
        void CreatePoolBullets()
        {
            for (int i = 0; i < _cBullet; i++)
            {
                var d =Instantiate(_bullet, _spawnBolt, Quaternion.identity, transform);
                _poolBullets.Add(d);
                Helper.SetActive(d, false);
            }
        }
        
        // получить объект из пула
        GameObject GetBullet()
        {
            for (int i = 0; i < _cBullet; i++)
            {
                if (!_poolBullets[i].activeInHierarchy)
                {
                    _poolBullets[i].transform.position = _spawnBolt;
                    Helper.SetActive(_poolBullets[i], true);
                    return _poolBullets[i];
                }
            }
            var a = Instantiate(_bullet, _spawnBolt, Quaternion.identity, transform);
            return a;
        }

        // проверить находится ли цель в зоне поражения
        bool CheckArea()
        {
            if (!targetEnemy)
            {
                return false;
            }
            return Vector3.Distance(targetEnemy.transform.position, transform.position) < _affectedArea;
        }
        
        // сканировать зону поражения
        bool ScanArea()
        {
            return Physics.CheckSphere(transform.position, _affectedArea, _enemyLayer);
        }

        // выключить возможность нажимать на объект
        void OffColider()
        {
            GetComponent<BoxCollider>().enabled = false;
        }
 
        // осуществить подписку на единый Update для всей игры
        void subscription()
        {
            GameController.OnUpdate += UpdateScan;
            UIController.CheckMoney += OffUpgrade;
        }

        private void Awake()
        {
            Helper.SetActive(UpgrateUI, false);
        }

        void OffUpgrade(bool b)
        {
            if (LvlTower< _lvlModels.Count-1)
            {
                Helper.SetActive(UpgrateUI, b);                
            }
        }
    }
}