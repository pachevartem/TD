using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ArtelVR
{
    
    /// <summary>
    /// Описание ячейки
    /// </summary>
    public class CellExample: MonoBehaviour
    {

        private TowerExample _typeTower;
        private List<GameObject> _levelsModel;              //TODO: select a location for initialization
        private Vector3 _spawnBolt;                         //TODO: ctor inicializate
        private LayerMask _enemyLaer;
        private float _afectedArea;                         //TODO: ctor;
        
        
        void SetupTypeTower(TowerExample towerExample)      //TODO: просто описать в конструкторе если будет нужно
        {
            _typeTower = towerExample;
        }
        
        void CreateModels(List<GameObject> obj, Transform parent) //TODO: move to ctor
        {
            for (int i = 0; i < obj.Count; i++)
            {
               Instantiate(_levelsModel[i], parent.transform.position, Quaternion.identity, parent).SetActive(false);
            }
        }

        private void OnDrawGizmosSelected() //TODO: delete
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _afectedArea);
        }
        
        
    }
}