using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] AudioSource music;
    bool gameStarted = false;

    private void Update() {
        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && !gameStarted) {
            StartGame();
        }
    }
    private void StartGame() {
        gameStarted = true;
        canvas.GetComponent<Animator>().SetTrigger("StartGame");
        music.Play();
    }
}
