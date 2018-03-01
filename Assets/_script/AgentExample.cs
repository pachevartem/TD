using UnityEngine;

namespace ArtelVR
{
    [CreateAssetMenu(fileName = "Agent", menuName = "ArtelVR/EnemyType", order = 2)]
    public class AgentExample: ScriptableObject, IGetObj
    {
        [Header("Имя врага")] 
        public string Name;
       
        [Header("Модель врага")] 
        public GameObject ModelEnemy;
        
        [Header("Здоровье")] 
        public float Health;
        
        [Header("Урон")] 
        public float Damage;

        public GameObject GetModel()
        {
            return ModelEnemy;
        }
    }
}    