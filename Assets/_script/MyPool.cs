using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    public class MyPool<T> //TODO: объяснить на примере без T 
    {
//        
//        private List<GameObject> _items = new List<GameObject>();
//        private int Count;
//        private Vector3 _startPosition;
//
//        private T _mytype;
//
//        public MyPool(T item)
//        {
//            _mytype = item;
//        }
//        
//        private void SetupPool(int startcount)
//        {
//            for (int i = 0; i < startcount; i++)
//            {
//                CreateObj();
//            }
//        }
//
//        GameObject CreateObj()
//        {
//            var obj = new GameObject(typeof(T).Name, typeof(T));
//            obj.SetActive(false);
//            obj.transform.position = _startPosition;
//            _items.Add(obj);
//            return obj;
//        }
//
//        public GameObject GetObj()
//        {
//            for (int i = 0; i < _items.Count; i++)
//            {
//                if (!_items[i].activeInHierarchy)
//                {
//                    return _items[i];
//                }
//            }
//            return CreateObj();
//        }
//
//

    }
}