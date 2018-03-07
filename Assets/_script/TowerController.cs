using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
 

    public class TowerController: MonoBehaviour
    {

        protected List<GameObject> _models;
        protected GameObject _bolt;
        protected LayerMask _enemyLayer;
        protected float _affectedArea;
        protected float _damage;
        protected float _firedelay;

        [SerializeField]
        private TowerExample _typeExample;


        private void Construictor(TowerExample te)
        {
            _bolt = te.Bolt;
            _enemyLayer = te.EnemyLayer;
            _affectedArea = te.Raduis;
            _damage = te.Damages[0];
            _firedelay = te.FireDelay;
            CreateTower(te);
        }


        protected void Awake()
        {
            Construictor(_typeExample); //TODO: change to Event
        }

        void CreateTower(TowerExample t)
        {
            _models = new List<GameObject>();
            for (int i = 0; i < t.Levels.Count; i++)
            {
                _models.Add(Instantiate(t.Levels[i], transform.position, Quaternion.identity, transform));
                Helper.SetActive(_models[i], false);
            }
            Helper.SetActive(_models[0],true);
        }
        
        
    }
}