using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterData", menuName = "Scriptable Objects/PlayerCharacterData")]
public class PlayerCharacterData : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;

    [Header("Stats")]
    public float playerHP;

    //[Header("Starting Equipment")]
    //public 
}
