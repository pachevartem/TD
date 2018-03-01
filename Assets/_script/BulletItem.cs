using UnityEngine;

namespace ArtelVR
{
    public class BulletItem: MonoBehaviour
    {
        private void OnEnable()
        {
            Invoke("Off",1);
        }

        public void Off()
        {
            gameObject.SetActive(false);
        }
      
        public LayerMask Agent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer.CompareTo(Agent) == 1)
            {
                Off();
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}