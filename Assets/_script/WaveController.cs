using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class WaveController: MonoBehaviour
    {
        public List<AgentExample> EnemyType = new List<AgentExample>();
        private int _countWave;
        private int _countEnemy = 100; // TODO: assdas
        public float SpawnDelay = 2f; 

        public GameObject SpawnZone; //TODO: change in ctor from terrain
        private Animator _animator;

        
        private List<GameObject> _poolEnemys;

        private List<List<GameObject>> __enemyPools;

        void __CreatePools()
        {
            __enemyPools = new List<List<GameObject>>();
            for (int i = 0; i < GameController.Instance.GameSettings.EnemyType.Count; i++)
            {
                __enemyPools.Add(new List<GameObject>());
            }
            
        }


        GameObject CreateEnemys(AgentExample ae)
        {
            var obj = Instantiate(ae.ModelEnemy, SpawnZone.transform.position, Quaternion.identity);
            obj.name = ae.Name + " - ENEMY";
            obj.AddComponent<AgentController>();
            return obj;
        }
        
        void FillPool(List<GameObject> lg)
        {
//            _poolEnemys = new List<GameObject>();
            for (int i = 0; i < _countEnemy; i++)
            {
//                _poolEnemys.Add(CreateEnemys(EnemyType[0])); //TODO: Почему тут так?
//                _poolEnemys[i].SetActive(false); 
                
                _poolEnemys.Add(CreateEnemys(EnemyType[0])); //TODO: Почему тут так?
                _poolEnemys[i].SetActive(false);
            }
        }
        
        GameObject GetPoolObj()
        {
            for (int i = 0; i < _poolEnemys.Count; i++)
            {
                if (!_poolEnemys[i].activeInHierarchy)
                {
                    return _poolEnemys[i];
                }
            }
            return CreateEnemys(EnemyType[0]); //TODO: Тоже гавно?
        }

        IEnumerator GenerateWave()
        {
            for (int i = 0; i < _countWave; i++)
            {
                
            }
            yield break;
        }

        IEnumerator Born()
        {
            for (int i = 0; i < _countEnemy; i++)
            {
                GetPoolObj().SetActive(true);
                yield return new WaitForSeconds(SpawnDelay);
            }
            yield return null;
        }
        
        void Start()
        {
            _countWave = GameController.Instance.GameSettings.CountWave;
            FillPool();
            StartCoroutine(Born());
        }

    }
}