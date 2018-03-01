using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class WaveController: MonoBehaviour //TODO: Все к херам захешеровать, после того как будет работать
    {
        public List<MyPool> Enemys = new List<MyPool>();
        
        void SetupPool()
        {
            GameSettings _settings = GameController.Instance.GameSettings;
            for (int i = 0; i < _settings.EnemyType.Count; i++)
            {
                Enemys.Add(new MyPool(_settings.EnemyType[i], (new GameObject(i+"-Type")).transform, GameController.Instance.SpawnEnemys.transform.position, 2)); 
            }    
        }

        IEnumerator GenerateWaves() //TODO: организовать полную связь с ScriptableObject
        {
            print("Start Wave");
            for (int i = 0; i < GameController.Instance.GameSettings.WaveSettings.Waves.Count; i++)
            {
                List<int> sequence = Helper.RandomBettwen(0, GameController.Instance.GameSettings.EnemyType.Count , GameController.Instance.GameSettings.WaveSettings.Waves[i].CountEnemy);
                yield return StartCoroutine(born(sequence));   
                yield return new WaitForSeconds(3);
            }
            print("End wave");
        }
        
        IEnumerator born(List<int> seq)
        {
            for (int i = 0; i < seq.Count; i++)
            {
                print("Item -" + i);

                Enemys[seq[i]].GetPoolObject();
                yield return new WaitForSeconds(1);
            }
            
        }


        private void Awake()
        {
            SetupPool();
            StartCoroutine (GenerateWaves());
        }
    }
}