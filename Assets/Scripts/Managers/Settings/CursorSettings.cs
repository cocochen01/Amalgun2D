using Amalgun2D.Settings;
using UnityEngine;

public class CursorSettings : MonoBehaviour
{
    void Start()
    {
        SetCursor();
    }

    public void SetCursor()
    {
        if (SettingsManager.Instance.cursorData != null && SettingsManager.Instance.cursorData.cursorTextures.Length > 0)
        {
            Texture2D cursorTexture = SettingsManager.Instance.cursorData.cursorTextures[(int)SettingsManager.Instance.cursorType];

            if (cursorTexture != null)
            {
                Vector2 hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
                Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
            }
        }
    }
}
