using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++){
            if (i < health){
                hearts[i].sprite = fullheart;
            } else {
                hearts[i].sprite = emptyheart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
}
