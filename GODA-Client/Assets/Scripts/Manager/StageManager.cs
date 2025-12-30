using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerAnimTest player;
    [SerializeField] private RoomManager answerRoom;

    public bool StageClear = false;
    public bool lose = false;

    private void Update()
    {
        if (player.NowHp <= 0)
        {
            lose = true;
            StageClear = false;
        }
        else if (answerRoom.thisRoomClear)
        {
            StageClear = true;
        }
    }
}
