using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ArtelVR
{
    public class WaveController: MonoBehaviour
    {
        public List<AgentExample> EnemyType = new List<AgentExample>();
        private int CountEnemy = 10;
        public float SpawnDelay = 2f;

        public GameObject SpawnZone; //TODO: change in ctor from terrain
        private Animator _animator;
        private List<GameObject> Enemys = new List<GameObject>();
       
        
        GameObject CreateEnemys(AgentExample ae)
        {
            var obj = Instantiate(ae.ModelEnemy, SpawnZone.transform.position, Quaternion.identity);
            obj.name = ae.Name + " - ENEMY";
            obj.AddComponent<AgentController>();
            return obj;
        }

        void CreateWave()
        {
            for (int i = 0; i < CountEnemy; i++)
            {
                Enemys.Add(CreateEnemys(EnemyType[0]));
                Enemys[i].SetActive(false);
            }
        }

        private int k = 0;
        void StartWave()
        {
            if (k > CountEnemy-1)
            {
                return;
            }
            Enemys[k].SetActive(true);
            k++;
        }

        
        void Start()
        {
            CreateWave();
            InvokeRepeating("StartWave",2,1);
        }


    }
}