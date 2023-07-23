using UnityEngine;

public class Hold : MonoBehaviour
{
    private RectTransform buttonRectTransform;
    public bool onHold = false;

    void Start()
    {
        buttonRectTransform = this.GetComponent<RectTransform>();
    }

    public void HoldDice()
    {

        if (this.tag == "leftDice" && Dicer.round_stage == 1)
        {
            if (onHold == false)
            {
                Vector3 newAnchoredPosition = buttonRectTransform.anchoredPosition;
                newAnchoredPosition.x -= 4f;
                buttonRectTransform.anchoredPosition = newAnchoredPosition;
                onHold = true;
            }
        }

        if (this.tag == "rightDice" && Dicer.round_stage == 1)
        {
            if (onHold == false)
            {
                Vector3 newAnchoredPosition = buttonRectTransform.anchoredPosition;
                newAnchoredPosition.x += 4f;
                buttonRectTransform.anchoredPosition = newAnchoredPosition;
                onHold = true;
            }
        }


    }

}
