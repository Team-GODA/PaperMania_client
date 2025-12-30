using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerAnimTest player;

    [SerializeField] private Image skill1Image;
    [SerializeField] private Image skill2Image;
    public Image HPbar;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerAnimTest>();
    }

    private void Update()
    {
        skill1Image.fillAmount = 1- player.skill1Timer / player.Skill1Cooldown;
        skill2Image.fillAmount = 1 -player.skill2Timer / player.Skill2Cooldown;

        HPbar.fillAmount = player.NowHp / player.MaxHp;
    }
}
