using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
{
    enum GameState
    {
        Title,
        InGame,
        ScoreRanking,
    }
    GameState gameState = GameState.Title;

    private int START_SCORE = 0;

    [Header("スプレッドシートのスコア管理スクリプト")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    [Header("プレイヤー名入力のフィールド")]
    [SerializeField] private InputField nameInputField;

    //表示するtext
    [SerializeField] private Text playerName;

    void Start()
    {
        
    }

    
    void Update()
    {
        switch (gameState)
        {
            case GameState.Title:

                break;
            case GameState.ScoreRanking:

                break;
            case GameState.InGame:

                break;
        }
    }

    //名前表示
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;
    }
}
