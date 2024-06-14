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

    [Header("�X�v���b�h�V�[�g�̃X�R�A�Ǘ��X�N���v�g")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    [Header("�v���C���[�����͂̃t�B�[���h")]
    [SerializeField] private InputField nameInputField;

    //�\������text
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

    //���O�\��
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;
    }
}
