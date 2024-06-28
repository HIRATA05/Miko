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
    //�Q�[���X�e�[�g���
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private GameObject RankingPanel;

    //�\���̂̒�`
    public struct ScoreInfo 
    {
        public string name;
        public int score;

        public ScoreInfo(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    ScoreInfo scoreInfo = new ScoreInfo("", 0);

    struct RankingText
    {
        public Text name;
        public Text score;
    }
    [SerializeField] private RankingText rankingText;

    [SerializeField] private Text rankingNames;
    [SerializeField] private Text rankingScores;
    //�X�R�A�����L���O�̃��X�g�@10�ʂ܂œ���ĕ\������
    public static List<ScoreInfo> ScoreRanking = new List<ScoreInfo>();

    //���̃X�R�A
    [SerializeField] private Text ViewScore;
    private int SCORE = 0;

    [Header("�X�v���b�h�V�[�g�̃X�R�A�Ǘ��X�N���v�g")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    [Header("�v���C���[�����͂̃t�B�[���h")]
    [SerializeField] private InputField nameInputField;

    //�\������text
    [SerializeField] private Text playerName;

    void Start()
    {
        gssa_Score.DataGet();
    }

    
    void Update()
    {
        switch (gameState)
        {
            case GameState.Title://���O����
                if (!TitlePanel.activeSelf) TitlePanel.SetActive(true);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //�L�[�Ő؂�ւ�
                if (Input.GetKeyDown(KeyCode.Q)) { gameState = GameState.InGame; }
                break;
            case GameState.InGame://�����L���O�Ɉړ���scoreInfo�̃f�[�^���V�[�g�ɏo��
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (!InGamePanel.activeSelf) InGamePanel.SetActive(true);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //�L�[�Ő؂�ւ�
                if (Input.GetKeyDown(KeyCode.P)) { gameState = GameState.ScoreRanking; }
                break;
            case GameState.ScoreRanking://�����L���O�ɔ��f�@
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (!RankingPanel.activeSelf) RankingPanel.SetActive(true);

                //�����L���O�̃��X�g��10�ʂ܂ō쐬
                for (int i = 0; i < 10; i++)
                {

                }

                //�X�R�A��\��
                rankingNames.text = scoreInfo.name;
                rankingScores.text = scoreInfo.score.ToString();
                break;
        }
    }

    //���O�\��
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;
        scoreInfo.name = playerName.text;
    }
    //�X�R�A���Z�{�^��
    public void ScoreAddButton()
    {
        SCORE += 1;
        ViewScore.text = SCORE.ToString();
        scoreInfo.score = SCORE;
    }
}
