using UnityEngine;
using System.Collections;

public class Character1Skills : MonoBehaviour
{
    public AttackTarget attackTarget;
    public GameObject pencil;
    Vector3 penciloriginalPos;

    private void Start()
    {
        pencil.SetActive(false);
        penciloriginalPos = pencil.transform.position;
    }

    public IEnumerator Skill2()
    {
        Vector3 targetPos = new Vector3(5.28f, -2.43f, 0f);
        pencil.SetActive(true);

        float speed = 10f;
        while (Vector3.Distance(pencil.transform.position, targetPos) > 0.05f)
        {
            pencil.transform.position = Vector3.MoveTowards(pencil.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        attackTarget.AOEAttack("mid", 10);

        yield return new WaitForSeconds(0.4f);
        pencil.SetActive(false);
        pencil.transform.position = penciloriginalPos;
    }
}
