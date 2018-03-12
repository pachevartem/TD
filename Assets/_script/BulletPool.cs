using System.Collections.Generic;
using UnityEngine;

namespace BSU
{

    public class BulletPool: Pool, IGetBullet
    {
        
        private List<Bullet> _bullet = new List<Bullet>() ;


        public Bullet GetBullet()
        {
            return GetPoolObject().GetComponent<Bullet>();
        }

        public BulletPool(IGetObj getObj, Transform parent, Vector3 startPosition, int countpool) : base(getObj, parent, startPosition, countpool)
        {
            
        }
        
        
    }
}