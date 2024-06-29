using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class TextItemsCreated : MonoBehaviour
{
    [SerializeField] private string _header;

    private TMP_Text _label;

    private void Awake()
    {
        _label = GetComponent<TMP_Text>();
    }

    protected void ChangeValue(int value)
    {
        _label.text = _header + value;
    }
}
