using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    //Обработчик нажатия на ячейку 
    public delegate void ClickCell(CellController cell);
    public delegate void GC();
    public delegate void Upgrade(bool b);
    
    
    public  static class Helper
    {
        
        public static void OffCollider(GameObject obj)
        {
            obj.GetComponent<BoxCollider>().enabled = false;
        }

        public static void SetActive(GameObject obj, bool value)
        {
            obj.SetActive(value);
        }
        
        public static void SetActive(Transform obj, bool value)
        {
            SetActive(obj.gameObject,value);
        }

        public static void FillQueqe(Collider[] colliders, ref Queue<GameObject> queue)
        {
            for (var i = 0; i < colliders.Length; i++)
            {
                queue.Enqueue(colliders[i].gameObject);
            }
        }

        public static bool ShootDelay(float delay, float deltaTime)
        {
            return deltaTime > delay;
        }

        public static List<int> RandomBettwen(int a, int b, int count)
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < count; i++)
            {
                arr.Add(UnityEngine.Random.Range(a,b));
            }
            return arr;
        }
        
       
        public static void LookTargetY(ref GameObject who, Transform target)
        {
            var _dir = Quaternion.LookRotation(target.position - who.transform.position);
            who.transform.rotation = new Quaternion(who.transform.rotation.x, _dir.y, who.transform.rotation.z, _dir.w);
        }
        

    }

    public interface IGetObj
    {
        GameObject GetModel();
    }
}