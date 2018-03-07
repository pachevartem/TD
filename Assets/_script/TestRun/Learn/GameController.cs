using UnityEngine;

namespace ArtelVR.TestRun.Learn
{
    public class GameController: MonoBehaviour
    {

        public static GameController Instance;
        public LayerMask EnemyLayer;
        public TypeTower tt;
        public Tower t1;
        
        
        
        
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
            t1 = new Tower(tt);
        }

        private int a = 0;
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                a++;
                t1.LevelUp(a);
            }

            t1.RotateGun();
            
        }
    }
}