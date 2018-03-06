using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    [CreateAssetMenu(fileName = "WaveSettings", menuName = "ArtelVR/WaveSettings", order = 2)]
    public class WaveSettings: ScriptableObject
    {
        [Header("Укажите задержку в секундах между волнами")]
        public float DelaySec;
        [Header("Укажи количество волн")]
        public List<Wave> Waves;
        
        
        [Serializable]
        public class Wave
        {
            [Header("Укажите количество врагов")] 
            public int CountEnemy;
            
            [Header("Укажите типы врагов")]
            public List<AgentExample> AgentType;
        }

    }
}