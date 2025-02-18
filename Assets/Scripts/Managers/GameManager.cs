using Cinemachine;
using UnityEngine;

namespace Amalgun2D.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameObject player;
        public Camera playerCamera;
        public CinemachineVirtualCamera virtualCamera;
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
        private void Start()
        {
            // In case references not set in editor
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            if (playerCamera == null)
            {
                playerCamera = Camera.main;
            }
        }
    } 
}
