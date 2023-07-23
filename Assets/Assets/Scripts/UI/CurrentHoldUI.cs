using TMPro;
using UnityEngine;

public class CurrentHoldUI : MonoBehaviour
{
    private TextMeshProUGUI TMPText;
    void Start()
    {
        TMPText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TMPText.text = "Current Hold " + Dicer.currentHold.ToString();
    }
}
