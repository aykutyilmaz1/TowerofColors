using UnityEngine;

namespace _3_Scripts
{
    public class TowerTileFactory : MonoBehaviour
    {
        private static TowerTileFactory _instance;

        [SerializeField] private TowerTilePool towerTilePool;
        [SerializeField] private SpecialTilePool specialTilePool;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // Don't destroy the object when loading a new scene
            }
            else
            {
                Destroy(gameObject); // Destroy any additional instances
            }
        }
        
        public static TowerTileFactory GetInstance()
        {
            return _instance;
        }
        
        public TowerTile GetTowerTile()
        {
            return towerTilePool.GetObjectFromPool();
        }
        
        public void ReturnTowerTile(TowerTile towerTile)
        {
            if(towerTile is ExplodingTile) specialTilePool.ReturnObjectToPool(towerTile);
            else towerTilePool.ReturnObjectToPool(towerTile);
        }
        
        public TowerTile GetSpecialTile()
        {
            return specialTilePool.GetObjectFromPool();
        }

        public void ResetPools()
        {
            towerTilePool.ResetPool();
            specialTilePool.ResetPool();
        }
    }
}