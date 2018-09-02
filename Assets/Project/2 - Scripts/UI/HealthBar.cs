using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Health __health;
    public Health Health
    {
        get
        {
            return (this.__health);
        }
        set
        {
            this.__health = value;
            this.__health.SetHealthBar(this);
        }
    }

    private RectTransform _rectTransform;
    private RectTransform _canvasRectTransform;

    private void Awake()
    {
        this._rectTransform = this.GetComponent<RectTransform>();
        if (this._image == null)
            Debug.LogWarning("Image missing!!");
        this._canvasRectTransform = this.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 tmp = Camera.main.WorldToViewportPoint(this.Health.transform.position);
        Vector2 pos = new Vector2(tmp.x * this._canvasRectTransform.sizeDelta.x - this._canvasRectTransform.sizeDelta.x * 0.5f, tmp.y * this._canvasRectTransform.sizeDelta.y - this._canvasRectTransform.sizeDelta.y * 0.5f);
        this._rectTransform.anchoredPosition = pos + this.Health.Offset;
    }

    public void UpdateHealth(int hitPoints, int maxHitPoints)
    {
        this._image.fillAmount = hitPoints / (float)maxHitPoints;
    }
}
