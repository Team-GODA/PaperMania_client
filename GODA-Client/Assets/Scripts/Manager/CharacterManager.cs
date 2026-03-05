using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : SingleMono<CharacterManager>
{
	public EndpointSO EndpointSO;
	[Header("모든 캐릭터")]	
	public List<CharacterResponse> characterList;

	[Header("보유 중인 캐릭터")]
	public List<CharacterResponse> ownCharacters;
	[Header("미보유 캐릭터")]
	public List<CharacterResponse> notOwnCharacters;

	public IEnumerator GetCharacterAll()
	{
		yield return APIConnector.instance.GetCoroutine<Response<AllCharacterResponse>>(
			endpoint: EndpointSO.CharacterEndPoint + EndpointSO.CharacterAllEndPoint,
			onSuccess: (response) =>
			{
				characterList = response.Data.characters;
			}, null, true);
	}

	public void ClearAllData()
	{
		characterList.Clear();
		ownCharacters.Clear();
		notOwnCharacters.Clear();
	}

	//public IEnumerator GetPlayerCharacter()
	//{
	//	yield return APIConnector.instance.GetCoroutine<Response<AllCharacterResponse>>(
	//		endpoint: EndpointSO.CharacterEndPoint + EndpointSO.CharacterAllEndPoint,
	//		onSuccess: (response) =>
	//		{
	//			ownCharacters = response.Data.characters;
	//		}, null, true);
	//}
}
