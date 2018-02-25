using UnityEngine;

namespace ArtelVR
{
    public class GameController: MonoBehaviour
    {

        public delegate void GC();

        public static event GC OnAwake = () => { };
        public static event GC OnStart = () => { };
        public static event GC OnUpdate = () => { };

        private void Awake()
        {
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