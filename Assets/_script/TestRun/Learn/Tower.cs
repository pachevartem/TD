using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR.TestRun.Learn
{
    public class Tower 
    {
        
        private int _lvl = 0;
        
        private List<float> _damage;
        private List<float> _affectedArea;
        private List<float> _fireSpeed;
        private List<float> _cost;
        private List<float> _delay;

        private Queue<GameObject> _queueEnemys;
        
        private List<GameObject> _modelsTower;
        private List<GameObject> _modelGun;
        private List<GameObject> _spawnBolt;
        
        private GameObject _targetEnemy;
        private GameObject _targetSpawn;
        private GameObject _currentGun;
//        private GameObject c; //TODO: target: заменить на цель и проверку состояний. 
        
        
        
        private float _dt = 0;
        
        private Transform _parent;
        
        private Vector3 _boltPos;

        public BulletPool _bullets;
        

        public Tower(TypeTower tt, Transform parent) //Constructor
        {
            _modelGun = new List<GameObject>();
            _spawnBolt = new List<GameObject>();
            _modelsTower = new List<GameObject>();
            _queueEnemys = new Queue<GameObject>();
            
            _parent = parent;
            _delay = tt.FireSpeed;
            _damage = tt.Damage;
            _affectedArea = tt.AffectedArea;
            _fireSpeed = tt.FireSpeed;
            _cost = tt.Cost;
            
            FillModel(tt);
            _targetSpawn = _spawnBolt[0];
            
            LevelUp(0);
            _bullets = new BulletPool (tt, parent, _boltPos, 5);
            _targetEnemy = null;
//            c =  new GameObject("Target");
//            _targetEnemy = c;

        }

        void FillModel(TypeTower tt)
        {
            BSU_Help.InstantiateList(out _modelsTower, tt.PrefabsTower, _parent);
            
            for (int i = 0; i < tt.PrefabsTower.Count; i++) 
            {
                _spawnBolt.Add(_modelsTower[i].transform.Find("spawnbolt").gameObject); //TODO: надо решить как нормально распарсить башню. Может еще один скриптабл. 
                _modelGun.Add(_modelsTower[i].transform.GetChild(0).Find("gun").gameObject); 
            }
        }

        public void LevelUp(int level)
        {
            if (level>=_modelsTower.Count)
            {
                return;
            }

            _lvl = level;
            _boltPos = _spawnBolt[level].transform.position;
            _currentGun = _modelGun[level];
            _targetSpawn = _spawnBolt[level];
            BSU_Help.ChangeLvl(_modelsTower, _lvl);  
        }

        public void RotateGun() // TODO: Subscribe on Update
        {
            if (_currentGun == null) // != null;
            {
                return;
            }
            
            BSU_Help.LookTargetY(ref _currentGun, _targetEnemy.transform);
        }

        public void Fire()
        {
            

            if (_dt > _delay[_lvl] && _targetEnemy!= null )
            {            
                Debug.Log(1);
                _bullets.GetBullet().Shoot(_targetEnemy,_targetSpawn.transform.position,20,100);
                _dt = 0;
            }
        }

        public void Update()
        {
            _dt += Time.deltaTime;
            
            
            if (Physics.CheckSphere(_parent.position, _affectedArea[_lvl], GameController.Instance.EnemyLayer))
            {
                var objs = Physics.OverlapSphere(_parent.position, _affectedArea[_lvl], GameController.Instance.EnemyLayer);
                
                if (objs.Length==0) return;

                for (var i = 0; i < objs.Length; i++)
                {
                    _queueEnemys.Enqueue(objs[i].gameObject);
                }
                _targetEnemy = _queueEnemys.Peek();
            }
            else
            {
                return;
            }
            
            Fire();
            RotateGun();
            _queueEnemys.Clear();
        }

        









    }
}