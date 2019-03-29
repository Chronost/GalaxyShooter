using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //======================== VIDAS ==========================
    public Sprite[] lives;
    public Image livesImageDisplay;
    //======================== PONTOS ==========================
    public Text pontosText;
    public int pontos;


    public void updateLives(int currentLives)
    {
        Debug.Log("Vidas do Player: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }


    public void updateScore()
    {
        pontos += 10;
        pontosText.text = "Pontuação: " + pontos;
    }

}
