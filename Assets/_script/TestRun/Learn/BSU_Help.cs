using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR.TestRun.Learn
{
    public static class BSU_Help
    {
        public static void InstantiateList(out List<GameObject> fill, List<GameObject> prefabs, Transform parent)
        {
            fill  = new List<GameObject>();
            for (int i = 0; i < prefabs.Count; i++)
            {
                var obj = GameObject.Instantiate(prefabs[i], parent);
                obj.SetActive(false);
                fill.Add(obj);
            }
            fill[0].SetActive(true); //TODO: выключаюсь
            
        }

        public static void ChangeLvl(List<GameObject> select, int current)
        {
            foreach (var o in select)
            {
                o.SetActive(false);
            }
            select[current].SetActive(true);
        }
        
        public static void LookTargetY(ref GameObject who, Transform target)
        {
            var _dir = Quaternion.LookRotation(target.position - who.transform.position);
            who.transform.rotation = new Quaternion(who.transform.rotation.x, _dir.y, who.transform.rotation.z, _dir.w);
        }

        public static GameObject PrioritySearch(Collider[] colliders)
        {
            float max = float.MaxValue;
            int search = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                var current = colliders[i].gameObject.GetComponent<Agent>().GetDistance();
                if (current < max)
                {
                    max = current;
                    search = i;
                }
            }
            return colliders[search].gameObject;

        }


    }
}