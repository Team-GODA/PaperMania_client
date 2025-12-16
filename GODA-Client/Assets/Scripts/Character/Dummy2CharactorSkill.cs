using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Dummy2CharactorSkill", menuName = "Scriptable Objects/Dummy2CharactorSkill")]
public class Dummy2CharactorSkill : CharactorSkill
{
    public override IEnumerator OnClickSkill()
    {
        Debug.Log("½ºÅ³»ç¿ë");
        Debug.Log("Çªµåµæ »×»× Çª½µ Çªµåµåµæ");
        yield break;
    }
}
