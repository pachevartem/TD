using System;
using System.Collections;
using System.Collections.Generic;
using ArtelVR;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject Enemy;
    public Transform Spawn;
    public GameObject CastlObj;
    
    public static Transform Castl;
    
    
    private void Awake()
    {
        Castl = CastlObj.transform;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy(2, 4));
    }


    IEnumerator SpawnEnemy(int sec, int countEnemy)
    {
        for (int i = 0; i < countEnemy; i++)
        {
            Instantiate(Enemy, Spawn.position, Quaternion.identity);
            yield return new WaitForSeconds(sec);
        }
    }
}
