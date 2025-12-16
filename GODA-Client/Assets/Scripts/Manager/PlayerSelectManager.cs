using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    public CharactorData SelectCharactorSO;
    [SerializeField] private Player player;

    private void Start()
    {
        PlayerSelected();
    }

    public void PlayerSelected()
    {
        player.CharactorAnimator.runtimeAnimatorController = SelectCharactorSO.DummyAnimator;

        player.MaxHp = SelectCharactorSO.MaxHp;
        player.NowHp = SelectCharactorSO.MaxHp;
        player.BaseAttack = SelectCharactorSO.baseAttack;
        player.AttackRange = SelectCharactorSO.AttackRange;
        player.AttackCool = SelectCharactorSO.AttackCool;
        player.Speed = SelectCharactorSO.Speed;
    }
}
