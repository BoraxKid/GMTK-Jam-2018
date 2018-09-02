using UnityEngine;
using UnityEngine.UI;

public class BuildBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public TurretBuilder Turret { private get; set; }

    private RectTransform _rectTransform;
    private RectTransform _canvasRectTransform;

    private void Awake()
    {
        if (this._image == null)
            Debug.LogWarning("Image missing!!");
        this._rectTransform = this.GetComponent<RectTransform>();
        this._canvasRectTransform = this.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 tmp = Camera.main.WorldToViewportPoint(this.Turret.transform.position);
        Vector2 pos = new Vector2(tmp.x * this._canvasRectTransform.sizeDelta.x - this._canvasRectTransform.sizeDelta.x * 0.5f, tmp.y * this._canvasRectTransform.sizeDelta.y - this._canvasRectTransform.sizeDelta.y * 0.5f);
        this._rectTransform.anchoredPosition = pos;
    }

    private void LateUpdate()
    {
        this._image.fillAmount = this.Turret.ProgressAmount;
    }
}
