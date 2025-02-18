using Amalgun2D.Core;
using UnityEngine;

namespace Amalgun2D.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }

        [Header("Audio Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f;
        [Range(0f, 1f)] public float effectsVolume = 1f;

        [Header("Visual Settings")]
        [Range(0f, 1f)] public float cameraShakeIntensity = 1f;

        public enum CursorType
        {
            cursor1, cursor2, cursor3
        }
        [Header("Cursor Settings")]
        public CursorData cursorData;
        public CursorType cursorType;

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
        void Start()
        {
            SetCursor();
        }

        public void SetCursor()
        {
            if (cursorData != null && cursorData.cursorTextures.Length > 0)
            {
                Texture2D cursorTexture = cursorData.cursorTextures[(int)cursorType];

                if (cursorTexture != null)
                {
                    Vector2 hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                }
            }
        }
    } 
}
