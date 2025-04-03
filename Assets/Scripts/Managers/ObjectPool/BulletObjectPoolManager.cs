using UnityEngine;
using UnityEngine.Pool;

namespace Amalgun2D.Pools
{
    public class BulletObjectPoolManager : MonoBehaviour
    {
        public static BulletObjectPoolManager Instance { get; private set; }
        //public ObjectPool<Bullet> playerBulletPool;
        //public ObjectPool<Bullet> enemyBulletPool;
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
