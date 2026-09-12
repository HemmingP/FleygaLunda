using UnityEngine;
using UnityEngine.UI;

public class BarHorizontalAmount : MonoBehaviour
{
    private float fullWidth;
    [SerializeField] private GameObject bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    public void SetBarAmount(float amount)
    {
        if (bar != null)
        {
            bar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullWidth * Mathf.Clamp01(amount));
        }
    }
}
