using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR.TestRun.Learn
{
    public class Tower
    {
        
        private List<float> _damage;
        private List<float> _affectedArea;
        private List<float> _fireSpeed;
        private List<float> _cost;
        
        private List<GameObject> _modelsTower;
        private List<GameObject> _modelGun;
        private List<GameObject> _spawnBolt;
        
        private GameObject _targetEnemy;
        
        private int _lvl = 0;

        private Vector3 _boltPos;
        private GameObject _currentGun;

        private MyPool _bullets;


        private GameObject c;
        
        public Tower(TypeTower tt) //Constructor
        {
            _modelGun = new List<GameObject>();
            _spawnBolt = new List<GameObject>();
            _modelsTower = new List<GameObject>();
            
            _damage = tt.Damage;
            _affectedArea = tt.AffectedArea;
            _fireSpeed = tt.FireSpeed;
            _cost = tt.Cost;
            
            FillModel(tt);
            LevelUp(0);
            _bullets = new MyPool(tt, new GameObject("for_pool").transform, _boltPos, 5); //TODO: поместить в родителя
            
            c =  new GameObject("Target");

        }

        void FillModel(TypeTower tt)
        {
            BSU_Help.InstantiateList(out _modelsTower, tt.PrefabsTower, new GameObject().transform); //TODO: поместить в родителя
            
            for (int i = 0; i < tt.PrefabsTower.Count; i++) //TODO: решить вопросы с количеством в Scriptable
            {
                Debug.Log(_modelsTower[i].name);
                _spawnBolt.Add(_modelsTower[i].transform.Find("spawnbolt").gameObject);
                _modelGun.Add(_modelsTower[i].transform.GetChild(0).Find("gun").gameObject); //TODO: гавно сранное - убил час
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
            BSU_Help.ChangeLvl(_modelsTower, _lvl);  
        }

       public void RotateGun() // TODO: Subscribe on Update
        {
            if (_currentGun == null) // != null;
            {
                return;
            }
            
            BSU_Help.LookTargetY(ref _currentGun, c.transform);
            
        }
        
   






    }
}