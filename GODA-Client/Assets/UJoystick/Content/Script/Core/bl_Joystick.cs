using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class bl_Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Settings")]
    [SerializeField, Range(10f, 300f)] private float Radius = 100f; // 픽셀 단위 반경
    [SerializeField, Range(0.01f, 1f)] private float SmoothTime = 0.15f; // 스무스 시간 (sec)
    [SerializeField, Range(0.5f, 4f)] private float OnPressScale = 1.5f;
    public Color NormalColor = new Color(1, 1, 1, 1);
    public Color PressColor = new Color(1, 1, 1, 1);
    [SerializeField, Range(0.05f, 1f)] private float ColorFadeDuration = 0.15f;

    [Header("Reference")]
    [SerializeField] private RectTransform StickRect; // 조이스틱 스틱(중심)
    [SerializeField] private RectTransform CenterReference; // 백그라운드 RectTransform (중앙 기준)
    [SerializeField] private Canvas m_Canvas; // 선택적으로 직접 연결 가능

    // privates
    private Vector2 centerAnchoredPos;
    private Vector2 currentVelocity;
    private bool returning = false;
    private int lastPointerId = int.MinValue;
    private Image stickImage;
    private Image backImage;
    private Vector3 pressScaleVector;
    private Coroutine scaleCoroutine;

    void Start()
    {
        if (StickRect == null || CenterReference == null)
        {
            Debug.LogError("StickRect and CenterReference must be assigned.");
            enabled = false;
            return;
        }

        if (m_Canvas == null)
        {
            // 자동으로 루트 캔버스 찾기
            m_Canvas = GetComponentInParent<Canvas>();
            if (m_Canvas == null)
            {
                Debug.LogError("Canvas not found in parents. Assign a Canvas.");
                enabled = false;
                return;
            }
        }

        pressScaleVector = Vector3.one * OnPressScale;
        backImage = GetComponent<Image>();
        stickImage = StickRect.GetComponent<Image>();

        if (backImage != null) backImage.CrossFadeColor(NormalColor, 0.01f, true, true);
        if (stickImage != null) stickImage.CrossFadeColor(NormalColor, 0.01f, true, true);

        // center의 anchoredPosition을 기준 위치로 사용
        centerAnchoredPos = CenterReference.anchoredPosition;
    }

    void Update()
    {
        // 반환 중이면 SmoothDamp로 원래 자리로 이동
        if (returning)
        {
            Vector2 current = StickRect.anchoredPosition;
            Vector2 target = centerAnchoredPos;
            StickRect.anchoredPosition = Vector2.SmoothDamp(current, target, ref currentVelocity, Mathf.Max(0.001f, SmoothTime));
            if (Vector2.Distance(StickRect.anchoredPosition, target) < 0.5f)
            {
                returning = false;
                StickRect.anchoredPosition = target;
                currentVelocity = Vector2.zero;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 첫 포인터만 처리 (마우스는 pointerId = -1)
        if (lastPointerId == int.MinValue)
        {
            lastPointerId = eventData.pointerId;
            returning = false;
            if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
            scaleCoroutine = StartCoroutine(ScaleJoystick(true));
            if (backImage != null) backImage.CrossFadeColor(PressColor, ColorFadeDuration, true, true);
            if (stickImage != null) stickImage.CrossFadeColor(PressColor, ColorFadeDuration, true, true);

            // 즉시 드래그 처리
            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != lastPointerId) return;

        // 화면 좌표 -> CenterReference 기준 로컬 좌표로 변환
        Vector2 localPoint;
        Camera cam = (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera) ? m_Canvas.worldCamera : null;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CenterReference, eventData.position, cam, out localPoint))
        {
            // localPoint는 CenterReference의 pivot 기준 로컬 좌표. anchoredPosition으로 바로 사용 가능.
            Vector2 offset = localPoint;
            // 제한 (반경)
            float effectiveRadius = Radius * m_Canvas.scaleFactor; // 캔버스 스케일 고려
            if (offset.magnitude > effectiveRadius)
            {
                offset = offset.normalized * effectiveRadius;
            }

            StickRect.anchoredPosition = centerAnchoredPos + offset;
        }
        else
        {
            // fallback: 마우스/터치 좌표를 그대로 약식으로 변환
            // (대부분 위에서 성공하므로 여기서는 간단히 무시)
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != lastPointerId) return;

        // 초기화 / 원위치로 반환
        lastPointerId = int.MinValue;
        returning = true;
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleJoystick(false));
        if (backImage != null) backImage.CrossFadeColor(NormalColor, ColorFadeDuration, true, true);
        if (stickImage != null) stickImage.CrossFadeColor(NormalColor, ColorFadeDuration, true, true);
    }

    IEnumerator ScaleJoystick(bool increase)
    {
        float t = 0f;
        float dur = Mathf.Max(0.02f, ColorFadeDuration);
        Vector3 from = StickRect.localScale;
        Vector3 to = increase ? pressScaleVector : Vector3.one;
        while (t < dur)
        {
            t += Time.deltaTime;
            StickRect.localScale = Vector3.Lerp(from, to, t / dur);
            yield return null;
        }
        StickRect.localScale = to;
    }

    /// <summary>
    ///  -1..1 정규화된 값으로 반환됩니다.
    /// </summary>
    public float Horizontal
    {
        get
        {
            Vector2 offset = StickRect.anchoredPosition - centerAnchoredPos;
            float effectiveRadius = Radius * m_Canvas.scaleFactor;
            return Mathf.Clamp(offset.x / effectiveRadius, -1f, 1f);
        }
    }

    public float Vertical
    {
        get
        {
            Vector2 offset = StickRect.anchoredPosition - centerAnchoredPos;
            float effectiveRadius = Radius * m_Canvas.scaleFactor;
            return Mathf.Clamp(offset.y / effectiveRadius, -1f, 1f);
        }
    }

    /// <summary>
    /// 현재 stick의 실제 픽셀 거리 (디버그용)
    /// </summary>
    public float CurrentDistance
    {
        get
        {
            return (StickRect.anchoredPosition - centerAnchoredPos).magnitude;
        }
    }
}
