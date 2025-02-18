using UnityEngine;

[CreateAssetMenu(fileName = "CursorData", menuName = "Scriptable Objects/CursorData")]
public class CursorData : ScriptableObject
{
    public Texture2D[] cursorTextures;
    public Texture2D GetCursorTexture(int index)
    {
        if (index >= 0 && index < cursorTextures.Length)
        {
            return cursorTextures[index];
        }
        return null;
    }
}
