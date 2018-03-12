using System.Collections.Generic;
using UnityEngine;

namespace BSU
{
    public class Tower 
    {
        
        private int _lvl = 0;
        
        private List<float> _damage;
        private List<float> _affectedArea;
        private List<float> _fireSpeed;
        private List<float> _cost;
        private List<float> _delay;
        
        private List<GameObject> _modelsTower;
        private List<GameObject> _modelGun;
        private List<GameObject> _spawnBolt;
        
        private GameObject _targetEnemy;
        private GameObject _targetSpawn;
        private GameObject _currentGun;
        
        private float _dt = 0;
        
        private Transform _parent;
        
        private Vector3 _boltPos;

        public BulletPool _bullets;
        

        public Tower(TypeTower tt, Transform parent) //Constructor
        {
            _modelGun = new List<GameObject>();
            _spawnBolt = new List<GameObject>();
            _modelsTower = new List<GameObject>();
           
           
            _parent = parent;
            _delay = tt.FireSpeed;
            _damage = tt.Damage;
            _affectedArea = tt.AffectedArea;
            _fireSpeed = tt.FireSpeed;
            _cost = tt.Cost;

            _damage = tt.Damage;
            
            FillModel(tt);
            _targetSpawn = _spawnBolt[0];
            
            LevelUp(0);
            _bullets = new BulletPool (tt, parent, _boltPos, 5);
            _targetEnemy = null;


        }

        void FillModel(TypeTower tt)
        {
            BSU_Help.InstantiateList(out _modelsTower, tt.PrefabsTower, _parent);
            
            for (int i = 0; i < tt.PrefabsTower.Count; i++)  
            {
                var find = _modelsTower[i].transform.GetComponentsInChildren<Transform>();
                foreach (var v in find)
                {
                    if (v.name == "gun")
                    {
                        _modelGun.Add(v.gameObject);
                    }

                    if (v.name == "spawnbolt")
                    {
                        _spawnBolt.Add(v.gameObject);
                    }
                }
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
            if (_currentGun == null) 
            {
                return;
            }
            
            BSU_Help.LookTargetY(ref _currentGun, _targetEnemy.transform);
        }

        public void Fire()
        {
            if (_dt > _delay[_lvl] && _targetEnemy!= null)
            {            

                _bullets.GetBullet().Shoot(_targetEnemy,_targetSpawn.transform.position,20,_damage[_lvl]);
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

                _targetEnemy = BSU_Help.PrioritySearch(objs);

            }
            else
            {
                return;
            }
            
            Fire();
            RotateGun();
        }
    }
}