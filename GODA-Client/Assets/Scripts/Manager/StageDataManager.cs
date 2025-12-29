using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;

public class StageDataManager : SingleMono<StageDataManager>
{
	[SerializeField] private StageInfo StageUI;
	[SerializeField] private EndpointSO endPointSO;

	public IEnumerator GetStageData(int first, int last)
	{
		if (StageUI == null) StageUI = FindFirstObjectByType<StageInfo>();

		yield return APIConnector.instance.GetCoroutine<Response<RewardResponseWrapper>>(
			endpoint: $"{endPointSO.Reward}/{first}/{last}",
			onSuccess: (response) =>
			{
				StageUI.UpdateUI(response.Data.stageReward);
			}, null, true);
	}
}
