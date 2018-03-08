using UnityEngine;

namespace ArtelVR.TestRun.Scriptable
{
    [CreateAssetMenu(fileName = "Тип врага", menuName = "BSU/Создать новое описание врага", order = 1)]
    public class TypeEnemy: ScriptableObject, IGetObj
    {
        public GameObject Model;
        public float Health;
        
        
        public GameObject GetModel()
        {
            return Model;
        }
    }
}