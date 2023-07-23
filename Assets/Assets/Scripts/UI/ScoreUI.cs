using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI TMPText;
    void Start()
    {
        TMPText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TMPText.text = "Total Score: " + Dicer.points.ToString();
    }
}
