using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class bl_Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Settings")]
    [SerializeField, Range(10f, 300f)] private float Radius = 100f;
    [SerializeField, Range(0.01f, 1f)] private float SmoothTime = 0.15f;
    [SerializeField, Range(0.5f, 4f)] private float OnPressScale = 1.5f;
    public Color NormalColor = new Color(1, 1, 1, 1);
    public Color PressColor = new Color(1, 1, 1, 1);
    [SerializeField, Range(0.05f, 1f)] private float ColorFadeDuration = 0.15f;

    [Header("Reference")]
    [SerializeField] private RectTransform StickRect;
    [SerializeField] private RectTransform CenterReference;
    [SerializeField] private Canvas m_Canvas;

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
        pressScaleVector = Vector3.one * OnPressScale;
        backImage = GetComponent<Image>();
        stickImage = StickRect.GetComponent<Image>();

        if (backImage != null) backImage.CrossFadeColor(NormalColor, 0.01f, true, true);
        if (stickImage != null) stickImage.CrossFadeColor(NormalColor, 0.01f, true, true);

        centerAnchoredPos = CenterReference.anchoredPosition;
    }

    void Update()
    {
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
        if (lastPointerId == int.MinValue)
        {
            lastPointerId = eventData.pointerId;
            returning = false;
            if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
            scaleCoroutine = StartCoroutine(ScaleJoystick(true));
            if (backImage != null) backImage.CrossFadeColor(PressColor, ColorFadeDuration, true, true);
            if (stickImage != null) stickImage.CrossFadeColor(PressColor, ColorFadeDuration, true, true);

            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != lastPointerId) return;

        Vector2 localPoint;
        Camera cam = (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera) ? m_Canvas.worldCamera : null;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CenterReference, eventData.position, cam, out localPoint))
        {
            Vector2 offset = localPoint;
            float effectiveRadius = Radius * m_Canvas.scaleFactor;
            if (offset.magnitude > effectiveRadius)
            {
                offset = offset.normalized * effectiveRadius;
            }

            StickRect.anchoredPosition = centerAnchoredPos + offset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != lastPointerId) return;

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

    public float CurrentDistance
    {
        get
        {
            return (StickRect.anchoredPosition - centerAnchoredPos).magnitude;
        }
    }
}
