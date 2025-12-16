using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CharactorSkill", menuName = "Scriptable Objects/CharactorSkill")]
public class CharactorSkill : ScriptableObject
{
    public virtual IEnumerator OnClickSkill()
    {
        Debug.Log("½ºÅ³»ç¿ë");
        Debug.Log("Äô Äâ°¡°­ ¿ì¿À¾Æ¾Æ¾Æ¾Ç Çª½´¿ì¿ì¿ì¿õ¤·");
        yield break;
    }
}
