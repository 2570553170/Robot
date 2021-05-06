using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// UI面板可退拽功能，将此脚本加到UI的底板上，整个UI面板将可随意拖拽
/// </summary>
public class UICanMove : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragTarget;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if (dragTarget == null) dragTarget = transform.GetComponent<RectTransform>();
        if (canvas == null) canvas = GameObject.Find("UIRoot").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 移动拖拽框的位置
        dragTarget.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 把当前选中的拖拽框显示在最前面
        dragTarget.SetAsLastSibling();
    }

}
