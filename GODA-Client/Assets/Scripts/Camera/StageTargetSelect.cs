using Unity.Cinemachine;
using UnityEngine;

public class StageTargetSelect : MonoBehaviour
{
	[SerializeField] private Transform[] stageTargets;
	[SerializeField] private Transform mainTarget;
	[SerializeField] private Vector3 targetOffset;
	[SerializeField] private StageInfo info;

	private CinemachineCamera virtualCamera;

	private void Start()
	{
		virtualCamera = GetComponent<CinemachineCamera>();
	}

	public void ChangeTarget()
	{
		virtualCamera.Follow = stageTargets[info.last-1];
		GetComponent<CinemachinePositionComposer>().TargetOffset = targetOffset;
	}
	
	public void CancelTarget()
	{
		Vector3 pos = mainTarget.position;
		pos.x = virtualCamera.Follow.position.x;

		mainTarget.position = pos;
		virtualCamera.Follow = mainTarget;
		GetComponent<CinemachinePositionComposer>().TargetOffset = Vector3.zero;
	}
}
