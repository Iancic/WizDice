using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Dicer : MonoBehaviour
{
    //sprites
    public static Sprite spriteDice1, spriteDice2, spriteDice3, spriteDice4, spriteDice5, spriteDice6;
    public Sprite[] sprites = { spriteDice1, spriteDice2, spriteDice3, spriteDice4, spriteDice5, spriteDice6 };

    //ui images
    public static Image Dice1, Dice2, Dice3, Dice4, Dice5, Dice6;
    public Image[] imagesUI = { Dice1, Dice2, Dice3, Dice4, Dice5, Dice6 };

    //dices
    public static GameObject Dice1A, Dice2A, Dice3A, Dice4A, Dice5A, Dice6A;
    public GameObject[] dices = { Dice1A, Dice2A, Dice3A, Dice4A, Dice5A, Dice6A };

    //ui animations
    public static GameObject Dice1Anim, Dice2Anim, Dice3Anim, Dice4Anim, Dice5Anim, Dice6Anim;
    public GameObject[] animationsUI = { Dice1Anim, Dice2Anim, Dice3Anim, Dice4Anim, Dice5Anim, Dice6Anim };

    //onHold
    public bool[] holds = { false, false, false, false, false, false };

    //rolls var
    public static int roll1, roll2, roll3, roll4, roll5, roll6;
    public int[] rolls = { roll1, roll2, roll3, roll4, roll5, roll6 };
    public int[] result_number = new int[6];
    public int[] result_number_counter = new int[6];

    //counters
    private int dice_random_number;
    private int max_dices = 6;
    public static int rounds = 1;
    public static int points = 0;
    public int round_result;
    public static int round_stage = 4; // 1=roll stage; 2=action stage; 3=enemyturn; 4=transition
    public bool onHold = false;
    public static int currentHold = 0;

    public void RefreshRound()
    {
        round_stage = 2;
        points = points + currentHold;
        Progress_Bar.IncrementProgress(currentHold);

        for (int i = 0; i < max_dices; i++)
        {
            animationsUI[i].SetActive(true);
                    if (dices[i].tag == "leftDice" && holds[i] == true)
                    {
                        Vector3 newAnchoredPosition = dices[i].GetComponent<RectTransform>().anchoredPosition;
                        newAnchoredPosition.x += 4f;
                        dices[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                        holds[i] = false;
                    }
                    else if (dices[i].tag == "rightDice" && holds[i] == true)
                    {
                        Vector3 newAnchoredPosition = dices[i].GetComponent<RectTransform>().anchoredPosition;
                        newAnchoredPosition.x -= 4f;
                        dices[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                        holds[i] = false;
                    }
        }

        rounds++;
        currentHold = 0;
    }

    public void Roll()
    {
        round_stage = 1;
        for (int i = 0; i < max_dices; i++)
        {
            if (holds[i] == false)
            {
                dice_random_number = UnityEngine.Random.Range(0, 6);
                rolls[i] = dice_random_number;
                animationsUI[i].SetActive(false);
                imagesUI[i].sprite = sprites[dice_random_number];
            }
        }

        currentHold += Count();

        //animatie hold
        var result_number = rolls.GroupBy(x => x).Where(g => g.Count() > 0).Select(x => x.Key + 1).ToList();
        var result_number_counter = rolls.GroupBy(x => x).Where(g => g.Count() > 0).Select(x => x.Count()).ToList();

        for (int j = 0; j < result_number.Count; j++)
        {
            for (int i = 0; i < max_dices; i++)
            {
                if (result_number[j] == rolls[i])
                {
                    if (dices[i].tag == "leftDice" && holds[i] == false)
                    {
                        Vector3 newAnchoredPosition = dices[i].GetComponent<RectTransform>().anchoredPosition;
                        newAnchoredPosition.x -= 4f;
                        dices[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                        holds[i] = true;
                    }
                    else if (dices[i].tag == "rightDice" && holds[i] == false)
                    {
                        Vector3 newAnchoredPosition = dices[i].GetComponent<RectTransform>().anchoredPosition;
                        newAnchoredPosition.x += 4f;
                        dices[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                        holds[i] = true;
                    }
                }
            }
        }


    }

    public int Count()
    {
        round_result = 0;
        var result_number = rolls.GroupBy(x => x).Where(g => g.Count() > 0).Select(x => x.Key + 1).ToList();
        var result_number_counter = rolls.GroupBy(x => x).Where(g => g.Count() > 0).Select(x => x.Count()).ToList();

        for (int i = 0; i < result_number.Count; i++)
        {
            //particularitati zar 1
            if (result_number_counter[i] == 3 && result_number[i] == 1)
                round_result += 1000;
            else if (result_number_counter[i] < 3 && result_number[i] == 1)
                round_result += 100 * result_number_counter[i];
            else if (result_number_counter[i] > 3 && result_number[i] == 1)
                round_result += 1000 * result_number_counter[i] - 2;

            //particularitate zar 5
            else if (result_number_counter[i] < 3 && result_number[i] == 5)
                round_result += 50 * result_number_counter[i];

            //particularitati generale
            else if (result_number_counter[i] == 3 && result_number[i] != 1)
                round_result += result_number[i] * 100;
            else if (result_number_counter[i] > 3 && result_number[i] != 1)
                round_result += result_number[i] * 100 * result_number_counter[i] - 2;
        }

        return round_result;
    }

}
