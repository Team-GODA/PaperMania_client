using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TestSupporterSkill", menuName = "Scriptable Objects/TestSupporterSkill")]
public class TestSupporterSkill : CharacterSkill
{
    public override IEnumerator OnClickSkill()
    {
        Debug.Log("¼­Æý½ºÅ³ ¹ßµ¿");
        Debug.Log("Á¤È­·Î Ä¡À¯ÇØ¿ä ½´¾Æ¾Æ¾Ç ¾²¾Æ¾Æ¾Ç ÃòÀÌÀ× ÃÒ¾Æ¾Æ¾Ç");
        yield break;
    }
}
