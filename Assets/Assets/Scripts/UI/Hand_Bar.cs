using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Bar : MonoBehaviour
{
    static private Slider slider;
    private ParticleSystem particleSys;

    private float FillSpeed = 1f;

    private TextMeshProUGUI slider_value_text;

    void Awake()
    {
        slider = this.GetComponent<Slider>();
        particleSys = GameObject.Find("Hand_Particles").GetComponent<ParticleSystem>();
        slider_value_text = GameObject.Find("Hand_Slider_Value").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //move the slider and animate particles
        if (slider.value < Dicer.currentHold)
        {
            slider.value += FillSpeed * 5;
            if (!particleSys.isPlaying)
                particleSys.Play();
        }
        else if (slider.value > Dicer.currentHold)
        {
            slider.value -= FillSpeed * 5;
            if (!particleSys.isPlaying)
                particleSys.Play();
        }
        else
            particleSys.Stop();

        //show the value of the slider
        slider_value_text.text = slider.value.ToString();
    }
}
