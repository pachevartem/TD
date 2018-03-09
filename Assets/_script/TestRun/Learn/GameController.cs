using System.Collections;
using System.Collections.Generic;
using ArtelVR.TestRun.Scriptable;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace ArtelVR.TestRun.Learn
{
    public class GameController: MonoBehaviour
    {

        public static GameController Instance;
        public LayerMask EnemyLayer;
        public LayerMask CellLayer;
        public List<TypeEnemy> Enemies;
        public TypeTower TT;
        
        private MyPool EnemyType1;
        private MyPool EnemyType2;

        public Camera Main;
        
        public Transform SpawnAgent;
        public Transform MainCastle;

        
        void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(Instance.gameObject);
            }
        }


        public List<Tower> Towers;
        
     
        
        
        private void Awake()
        {
            Singleton();
            Towers = new List<Tower>();
            EnemyType1 = new MyPool(GameController.Instance.Enemies[0],new GameObject("For Type1").transform, SpawnAgent.position,10);
            EnemyType2 = new MyPool(GameController.Instance.Enemies[1],new GameObject("For Type2").transform, SpawnAgent.position,10);
            StartCoroutine(GeneretaEnemy());    
           

        }

        
        
        private GameObject focusObj = null;
     
 
 
        void Update1()
        { 
 
            if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
            {
                focusObj = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
     
                if(Physics.Raycast(ray, out hit, Mathf.Infinity))  
                {
                    focusObj=hit.transform.gameObject;
                }
            }
    
            if(focusObj && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
               
            }
            
            if(focusObj && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                focusObj=null;
            }
  
        }
        
        
        void Update()
        {   
                Ray ray = Main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, CellLayer) && Input.GetMouseButtonUp(0))
            {
                Towers.Add(new Tower(TT, hit.collider.transform));
                Debug.DrawLine(ray.origin, hit.point);
            }

            if (Towers.Count <= 0) return;
            
            
            foreach (var t in Towers)
            {
                t.Update();
            }

        }

        IEnumerator GeneretaEnemy()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return GetRandomEnemy();
                yield return new WaitForSeconds(Random.Range(2,4));
            }
        }

        IEnumerator GetRandomEnemy()
        {
            for (int i = 0; i < 10; i++)
            {
                var a = Random.Range(0, 2);
                if (a==0)
                {
                    var s =EnemyType1.GetPoolObject();
                    s.GetComponent<Agent>().Health += Time.time;
                }
                else
                {
                    var s =EnemyType2.GetPoolObject();
                    s.GetComponent<Agent>().Health += Time.time;
                }

                yield return new WaitForSeconds(Random.Range(0f,1f));
            }
        }

        


    }
}