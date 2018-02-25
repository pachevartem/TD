using UnityEngine;

namespace ArtelVR
{
    [CreateAssetMenu(fileName = "Agent", menuName = "ArtelVR/EnemyType", order = 2)]
    public class AgentExample: ScriptableObject
    {
        [Header("Имя врага")] public string Name;
        [Header("Модель врага")] public GameObject ModelEnemy;
        [Header("Здоровье")] public float Health;
        [Header("Управляющий аниматор")] public Animator AnimatorEnemy;
        [Header("Нанесенный урон")] public float Damage;
    }
}    