using UnityEngine;

namespace ArtelVR
{
    public class GameController: MonoBehaviour
    {
        public static event GC OnAwake = () => { };
        public static event GC OnStart = () => { };
        public static event GC OnUpdate = () => { }; 

     
        
        [Header("Укажи место появление врагов")]
        public GameObject SpawnEnemys;
        
            
        
        public static GameController Instance;


        void Singleton()
        {
            if (!Instance)
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
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }
    }
}