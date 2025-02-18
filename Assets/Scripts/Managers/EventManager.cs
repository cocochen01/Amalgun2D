using UnityEngine;

namespace Amalgun2D.Core
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }
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
