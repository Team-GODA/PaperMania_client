using System.Collections;
using UnityEngine;

public class StageDataManager : SingleMono<StageDataManager>
{
	[SerializeField] private StageInfo StageUI;
	[SerializeField] private EndpointSO endPointSO;

	[SerializeField] private int currentStage;
	[SerializeField] private int currentStageSub;

	public IEnumerator GetStageData(int first, int last)
	{
		if (StageUI == null) StageUI = FindFirstObjectByType<StageInfo>();

		currentStage = first;
		currentStageSub = last;

		yield return APIConnector.instance.GetCoroutine<Response<RewardResponseWrapper>>(
			endpoint: $"{endPointSO.Reward}/{first}/{last}",
			onSuccess: (response) =>
			{
				StageUI.UpdateUI(response.Data.stageReward);
			}, null, true);
	}

	//public void GetStageReward()
	//{
	//	StartCoroutine(getStageRewardCoroutine());
	//}

	//private IEnumerator getStageRewardCoroutine()
	//{
	//	yield return APIConnector.instance.PostCoroutine<Response<ClaimStageRewardResponse>>(
	//		endPoint: $"{endPointSO.Reward}/{currentStage}/{currentStageSub}",
	//		onSuccess: (response) =>
	//		{
				
	//		},
	//		null, true
	//		);
	//}
}
