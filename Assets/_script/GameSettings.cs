using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class GameSettings: ScriptableObject
    {
        [Header("Количество набегов")]
        public int CountWave;
        [Header("Стартовый запас денег")]
        public float StartMoney;
        [Header("Тип врагов")]
        public List<AgentExample> EnemyType;
        [Header("Типы башень")]
        public List<TowerExample> TowerType;
        [Header("Звук окончания строительства")] 
        public AudioClip CompleteBuilding;
        
        
    }
}