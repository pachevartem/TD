using UnityEngine;

namespace ArtelVR.TestRun.Scriptable
{
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