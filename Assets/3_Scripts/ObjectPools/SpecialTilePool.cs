using UnityEngine;

namespace _3_Scripts
{
    public class SpecialTilePool : ObjectPool<TowerTile>
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