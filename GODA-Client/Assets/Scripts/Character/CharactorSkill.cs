using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CharacterSkill", menuName = "Scriptable Objects/CharacterSkill")]
public class CharacterSkill : ScriptableObject
{
    public virtual IEnumerator OnClickSkill()
    {
        Debug.Log("½ºÅ³»ç¿ë");
        Debug.Log("Äô Äâ°¡°­ ¿ì¿À¾Æ¾Æ¾Æ¾Ç Çª½´¿ì¿ì¿ì¿õ¤·");
        yield break;
    }
}
