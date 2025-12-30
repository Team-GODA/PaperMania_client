using UnityEngine;
using UnityEngine.Events;

public class StageManager : SceneSingleMono<StageManager>
{
    [SerializeField] private PlayerAnimTest player;
    [SerializeField] private RoomManager answerRoom;

    public bool StageClear = false;
    public bool lose = false;

    public UnityEvent OnStageClear;
    public UnityEvent OnStageFailed;

    private void Update()
    {
        if (player.NowHp <= 0)
        {
            lose = true;
            OnStageFailed?.Invoke();
        }
        else if (answerRoom.thisRoomClear)
        {
            OnStageClear?.Invoke();
        }
    }
}
