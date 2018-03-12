using System.Collections.Generic;
using UnityEngine;

namespace BSU
{
    public class Pool 
    {
        protected IGetObj _getObj;
        protected Vector3 startPosition;
        protected int countpool;
        protected Transform parent;
      
        protected List<GameObject> poolObj;

        public Pool(IGetObj getObj, Transform parent, Vector3 startPosition, int countpool)
        {
            _getObj = getObj;
            this.startPosition = startPosition;
            this.countpool = countpool;
            this.parent = parent;
            setPool();
        }
        
        
        void setPool()
        {
            poolObj = new List<GameObject>();
            for (int i = 0; i < countpool; i++)
            {
                CreateObj();
            }
        }
        protected GameObject CreateObj()
        {
            var obj = GameObject.Instantiate(_getObj.GetModel());
            obj.transform.SetParent(parent);
            obj.SetActive(false);
            obj.transform.position = startPosition;
            poolObj.Add(obj);
            return obj;
        }

        public GameObject GetPoolObject()
        {
            for (int i = 0; i < poolObj.Count; i++)
            {
                if (!poolObj[i].activeInHierarchy)
                {
                    poolObj[i].SetActive(true);
                    return poolObj[i];
                }
            }
            var obj = CreateObj();
                obj.SetActive(true);            
            return obj;
        }

    }
}