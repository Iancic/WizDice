using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Bar : MonoBehaviour
{
    static private Slider slider;
    private ParticleSystem particleSys;

    static private float targetProgress = 0;
    private float FillSpeed = 1f;

    private TextMeshProUGUI slider_value_text;

    void Awake()
    {
        slider = this.GetComponent<Slider>();
        particleSys = GameObject.Find("Particles").GetComponent<ParticleSystem>();
        slider_value_text = GameObject.Find("Slider_Value").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //move the slider and animate particles
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * 5;
            if (!particleSys.isPlaying)
                particleSys.Play();
        }
        else
            particleSys.Stop();

        //show the value of the slider
        slider_value_text.text = slider.value.ToString();
    }

    static public void IncrementProgress(int newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
