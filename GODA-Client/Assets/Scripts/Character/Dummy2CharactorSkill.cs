using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Dummy2CharacterSkill", menuName = "Scriptable Objects/Dummy2CharacterSkill")]
public class Dummy2CharacterSkill : CharacterSkill
{
    public override IEnumerator OnClickSkill()
    {
        Debug.Log("½ºÅ³»ç¿ë");
        Debug.Log("Çªµåµæ »×»× Çª½µ Çªµåµåµæ");
        yield break;
    }
}
