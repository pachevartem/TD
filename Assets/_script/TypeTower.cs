using System.Collections.Generic;
using UnityEngine;

namespace BSU
{
    
    [CreateAssetMenu(fileName = "Тип башни", menuName = "BSU/Создать новое описание башни", order = 1)]
    public class TypeTower : ScriptableObject, IGetObj
    {

//        Имя
//        Урон 
//        Радиус поражения 
//        Скорость стрельбы, раз/сек
//        Цена Покупки/Обновления  
//        Модель башни 
//        Модель Орудия
//        Тип снаряда

        [Header("Название башни")] public string NameTower;

        [Header("Урон")] public List<float> Damage;

        [Header("Радиус поражения башни")] public List<float> AffectedArea;

        [Header("Скорость стрельбы раз/сек")] public List<float> FireSpeed;

        [Header("Цена Покупки/Обновления")] public List<float> Cost;

        [Header("Модели башни")] public List<GameObject> PrefabsTower;

        [Header("Модель снаярда")] public GameObject BoltModel;

        public GameObject GetModel()
        {
            return BoltModel;
        }
    }
}