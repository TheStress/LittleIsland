using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnifeGame : MonoBehaviour
{
    [SerializeField] int countToWin;
    [SerializeField] GameObject food;
    [SerializeField] TMP_Text counterUI; 

    bool gameStarted = false;
    bool gameWin = false;
    int count = 0;

    public void StartGame() {
        count = 0;
        gameStarted = true;
        counterUI.gameObject.SetActive(true);
    }
    
    public bool IsStarted() {
        return gameStarted;
    }
    
    public void EndGame() {
        count = 0;
        gameStarted = false;
        counterUI.gameObject.SetActive(false);
    }

    public void CountIncrement() {
        count++;
        counterUI.text = count.ToString();

        // Checking win condition
        if(count >= countToWin && !gameWin) {
            gameWin = true;
            WinGame();
        }
    }

    private void WinGame() {
        food.SetActive(true);
    }
}
