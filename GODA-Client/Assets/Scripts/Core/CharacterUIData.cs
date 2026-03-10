using UnityEngine;

[CreateAssetMenu(fileName = "BasicCharacterData", menuName = "Scriptable Objects/Character UI Data")]
public class CharacterUIData : ScriptableObject
{
    public int Id;
    public Sprite MainArt;
    public CharacterResponse data;
}
