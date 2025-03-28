using UnityEngine;

[CreateAssetMenu(fileName = "GlobalValuesData", menuName = "Scriptable Objects/GlobalValuesData")]
public class GlobalValuesData : ScriptableObject
{
    // How quickly you can attack
    public const float globalAttackCD = .1f;
    // How quickly you can click UI elements
    public const float globalUIClickCD = .1f; // change to .02 later?
    // How long player input is stored in a buffer, use on discrete input not continuous 
    public const float globalInputBuffer = .1f;
}
