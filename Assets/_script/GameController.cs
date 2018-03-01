using UnityEngine;

namespace ArtelVR
{
    public class GameController: MonoBehaviour
    {
        public static event GC OnUpdate;

        [Header("Перетяни сюда файл с настройками игры настройки игры")]
        public GameSettings GameSettings;
        
        [Header("Укажи место появление врагов")]
        public GameObject SpawnEnemys;

        [Header("Укажи главный замок")]
        public GameObject MainCastle;
        
        [Header("Укажи слой, на котором находятся ячейки (CELL)")]
        public LayerMask CellLayerMask;
        
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
        }
        
        private void Update()
        {
            if (OnUpdate == null) 
            {
                return;               
            }
            OnUpdate();
        }
    }
}