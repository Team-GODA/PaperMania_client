using Unity.Cinemachine;
using UnityEngine;

public class StageTargetSelect : MonoBehaviour
{
	[SerializeField] private Transform[] stageTargets;
	[SerializeField] private Transform mainTarget;
	[SerializeField] private Vector3 targetOffset;

	private CinemachineCamera virtualCamera;

	private void Start()
	{
		virtualCamera = GetComponent<CinemachineCamera>();
	}

	public void ChangeTarget(int idx)
	{
		virtualCamera.Follow = stageTargets[idx];
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
