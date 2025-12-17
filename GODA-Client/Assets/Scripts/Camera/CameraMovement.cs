using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[Tooltip("드래그 이동 비율")]
	[SerializeField] private float moveRate = 10f;

	[Tooltip("시네머신 가상 카메라")]
	[SerializeField] private CinemachineCamera virtualCamera;

	private Vector2 clickPos;
	private Vector3 targetStartPos;

	private Transform followTarget;

	private void Awake()
	{
		followTarget = virtualCamera.Follow;
	}

	void Update()
	{
		TouchMovement();
	}

	private void TouchMovement()
	{
		if (Input.touchCount != 1) return;

		Touch touch = Input.GetTouch(0);

		// 터치 시작
		if (touch.phase == TouchPhase.Began)
		{
			clickPos = touch.position;
			targetStartPos = followTarget.position;
		}

		// 드래그
		else if (touch.phase == TouchPhase.Moved)
		{
			Vector2 delta = clickPos - touch.position;

			Vector3 move = new Vector3(delta.x, 0f, 0f) * moveRate * Time.deltaTime;

			followTarget.position = targetStartPos + move;
		}
	}
}
