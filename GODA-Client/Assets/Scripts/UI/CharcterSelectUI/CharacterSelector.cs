using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
	// 현재 캐릭터 정보를 저장하는 역할의 코드가 없어 임시로 게임오브젝트 클래스를 사용
	// 추후 개발이 완료되면 변경 예정
	private GameObject mainCharacter;
	private List<GameObject> subCharacterList = new List<GameObject>();

	public void ChangeMainCharacter(GameObject character)
	{
		mainCharacter = character;

		// UI 업데이트 함수(UI 관리자 참조)
	}

	public void AddSubCharacter(GameObject character)
	{
		if (subCharacterList.Count >= 2) subCharacterList.RemoveAt(0);
		subCharacterList.Add(character);
	}
}
