using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    
    [CreateAssetMenu(fileName = "Settings Game", menuName = "ArtelVR/GameSettings", order = 2)]
    public class GameSettings: ScriptableObject
    {
        [Header("Количество набегов")]
        public int CountWave;
        
        [Header("Стартовый запас денег")]
        public float StartMoney;

        [Header("Генератор волн")]
        public WaveSettings WaveSettings;
        
        [Header("Тип врагов")]
        public List<AgentExample> EnemyType;
        
        [Header("Типы башень")]
        public List<TowerExample> TowerType;
        
        [Header("Звук окончания строительства")] 
        public AudioClip CompleteBuilding;
        
        
    }
}