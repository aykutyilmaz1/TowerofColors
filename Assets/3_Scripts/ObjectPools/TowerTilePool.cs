using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3_Scripts
{
    public class TowerTilePool : ObjectPool<TowerTile>
    {
        public override void ResetPool()
        {
            base.ResetPool();
            
            foreach (TowerTile obj in _objectPool)
            {
                obj.ResetToBase();
            }
        }
        
        public override void ReturnObjectToPool(TowerTile obj)
        {
            base.ReturnObjectToPool(obj);
            obj.ResetToBase();
        }
    }
}