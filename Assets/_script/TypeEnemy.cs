using UnityEngine;

namespace BSU
{
    /// <summary>
    /// Файл настроек для создание врага
    /// </summary>
    [CreateAssetMenu(fileName = "Тип врага", menuName = "BSU/Создать новое описание врага", order = 1)]
    public class TypeEnemy: ScriptableObject, IGetObj
    {
        public GameObject Model;
        public float Health;
        
        /// <summary>
        /// Для Pool'a объектов
        /// </summary>
        /// <returns></returns>
        public GameObject GetModel()
        {
            return Model;
        }
    }
}