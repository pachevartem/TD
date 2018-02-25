using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
    //Обработчик нажатия на ячейку 
    public delegate void ClickCell(CellController cell);

    public delegate void SelectTarget(GameObject aim);
    
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
    }
}