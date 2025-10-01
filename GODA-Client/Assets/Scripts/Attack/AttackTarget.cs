using System.Collections;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    private Target1Attack attack1;
    private Target2Attack attack2;
    private Target3Attack attack3;
    private Target4Attack attack4;
    private Target5Attack attack5;
    private Target6Attack attack6;

    private void Awake()
    {
        attack1 = GameObject.Find("Target1").GetComponent<Target1Attack>();
        attack2 = GameObject.Find("Target2").GetComponent<Target2Attack>();
        attack3 = GameObject.Find("Target3").GetComponent<Target3Attack>();
        attack4 = GameObject.Find("Target4").GetComponent<Target4Attack>();
        attack5 = GameObject.Find("Target5").GetComponent<Target5Attack>();
        attack6 = GameObject.Find("Target6").GetComponent<Target6Attack>();
    }

    private void Start()
    {
        attack1.boxCol.enabled = false;
        attack2.boxCol.enabled = false;
        attack3.boxCol.enabled = false;
        attack4.boxCol.enabled = false;
        attack5.boxCol.enabled = false;
        attack6.boxCol.enabled = false;
    }

    public virtual void TargetAttack(int targetidx, float dmg)
    {
        switch (targetidx)
        {
            case 1:
                attack1.GetDmg(dmg);
                StartCoroutine(Target1Attack());
                break;
            case 2:
                attack2.GetDmg(dmg);
                StartCoroutine(Target2Attack());
                break;
            case 3:
                attack3.GetDmg(dmg);
                StartCoroutine(Target3Attack());
                break;
            case 4:
                attack4.GetDmg(dmg);
                StartCoroutine(Target4Attack());
                break;
            case 5:
                attack5.GetDmg(dmg);
                StartCoroutine(Target5Attack());
                break;
            case 6:
                attack6.GetDmg(dmg);
                StartCoroutine(Target6Attack());
                break;
            default:
                break;
        }
    }

    public virtual void AOEAttack(string attackPos, float dmg)
    {
        switch (attackPos)
        {
            case "front":
                attack1.GetDmg(dmg);
                attack2.GetDmg(dmg);
                StartCoroutine(Target1Attack());
                StartCoroutine(Target2Attack());
                break;
            case "mid":
                attack3.GetDmg(dmg);
                attack4.GetDmg(dmg);
                StartCoroutine(Target3Attack());
                StartCoroutine(Target4Attack());
                break;
            case "back":
                attack5.GetDmg(dmg);
                attack6.GetDmg(dmg);
                StartCoroutine(Target5Attack());
                StartCoroutine(Target6Attack());
                break;
            default:
                break;
        }
    }

    IEnumerator Target1Attack()
    {
        attack1.boxCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack1.boxCol.enabled = false;
    }
    IEnumerator Target2Attack()
    {
        attack2.boxCol.enabled = true; 
        yield return new WaitForSeconds(0.1f);
        attack2.boxCol.enabled = false;
    }
    IEnumerator Target3Attack()
    {
        attack3.boxCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack3.boxCol.enabled = false;
    }
    IEnumerator Target4Attack()
    {
        attack4.boxCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack4.boxCol.enabled = false;
    }
    IEnumerator Target5Attack()
    {
        attack5.boxCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack5.boxCol.enabled = false;
    }
    IEnumerator Target6Attack()
    {
        attack6.boxCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack6.boxCol.enabled = false;
    }
}
