using TMPro;
using UnityEngine;

public class RoundsUI : MonoBehaviour
{
    private TextMeshProUGUI TMPText;
    void Start()
    {
        TMPText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TMPText.text = "Round " + Dicer.rounds.ToString();
    }
}
