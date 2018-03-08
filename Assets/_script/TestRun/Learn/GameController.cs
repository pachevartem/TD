using System.Collections.Generic;
using ArtelVR.TestRun.Scriptable;
using UnityEngine;
using UnityEngine.AI;

namespace ArtelVR.TestRun.Learn
{
    public class GameController: MonoBehaviour
    {

        public static GameController Instance;
        public LayerMask EnemyLayer;

        public List<TypeEnemy> Enemies;
        
        
        
        void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(Instance.gameObject);
            }
        }

        private void Awake()
        {
            Singleton();
         
        }

        private void Update()
        {

        }


    }
}