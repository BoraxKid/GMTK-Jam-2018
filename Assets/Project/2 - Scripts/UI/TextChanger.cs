using UnityEngine;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _beforeIntText;
    [SerializeField] private string _afterIntText;

    public void ChangeText(int value)
    {
        this._text.SetText(this._beforeIntText + value + this._afterIntText);
    }
}
