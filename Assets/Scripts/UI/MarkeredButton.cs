using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarkeredButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClick = new();
    [SerializeField] private Image _marker;
    [SerializeField] private float _hoverScale;

    private void Awake()
    {
        _marker.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _marker.enabled = true;
        transform.localScale = Vector3.one * _hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _marker.enabled = false;
        transform.localScale = Vector3.one;
    }
}
