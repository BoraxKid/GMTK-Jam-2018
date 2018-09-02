using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private TurretPlacer _turretPlacer;

    private RectTransform _rectTransform;
    private RectTransform _canvasRectTransform;

    private void Awake()
    {
        this._rectTransform = this.GetComponent<RectTransform>();
        this._canvasRectTransform = this.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector2 tmp = Camera.main.WorldToViewportPoint(this._turretPlacer.Player.position);
        Vector2 pos = new Vector2(tmp.x * this._canvasRectTransform.sizeDelta.x - this._canvasRectTransform.sizeDelta.x * 0.5f, tmp.y * this._canvasRectTransform.sizeDelta.y - this._canvasRectTransform.sizeDelta.y * 0.5f);
        this._rectTransform.anchoredPosition = pos;
    }
}
