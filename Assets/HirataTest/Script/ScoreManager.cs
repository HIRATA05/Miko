using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
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
    //�Q�[�����̃X�R�A�f�[�^������
    ScoreInfo scoreInfo = new ScoreInfo("", 0);
    //�V�[�g����擾����
    public ScoreInfo[] getScoreInfo = new ScoreInfo[10];

    //�X�R�A�\�[�g�p�̃��X�g���쐬
    public List<ScoreInfo> Scorelist = new List<ScoreInfo>();

    //�����L���O�\���̃t���O
    bool RankingDisplay = false;

    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];
    //�X�R�A�����L���O�̃��X�g�@10�ʂ܂œ���ĕ\������
    public static List<ScoreInfo> ScoreRanking = new List<ScoreInfo>();

    //�����L���O�\���̑ҋ@���
    [SerializeField] private GameObject WaitPanal;

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
        RankingDisplay = false;
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Title://���O����
                if (!TitlePanel.activeSelf) TitlePanel.SetActive(true);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //���O�����͂���Ă��鎞�L�[�Ő؂�ւ�
                if (Input.GetKeyDown(KeyCode.Q) && nameInputField.text != "") { gameState = GameState.InGame; }
                break;
            case GameState.InGame://�����L���O�Ɉړ���scoreInfo�̃f�[�^���V�[�g�ɏo��
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (!InGamePanel.activeSelf) InGamePanel.SetActive(true);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //�L�[�Ő؂�ւ�
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //�X�R�A���X�v���b�h�V�[�g�ɕۑ�
                    gssa_Score.ChatLogSave(scoreInfo.name, scoreInfo.score);

                    gameState = GameState.ScoreRanking;
                }
                break;
            case GameState.ScoreRanking://�����L���O�ɔ��f�@
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (!RankingPanel.activeSelf) RankingPanel.SetActive(true);

                //�X�R�A���擾���Ă��Ȃ����V�[�g����擾����
                if (!RankingDisplay)
                {
                    RankingDisplay = true;

                    StartCoroutine(DisplayRanking());

                    //Invoke("DisplayRanking", 5.0f);
                }

                break;
        }
    }

    private IEnumerator DisplayRanking()
    {
        yield return gssa_Score.ChatLogGetIterator();
        //gssa_Score.DataGet();

        //�ҋ@��ʔ�\��
        if (WaitPanal.activeSelf)
        {
            WaitPanal.SetActive(false);
        }

        //�����L���O�̃��X�g��10�ʂ܂ŕ\��
        for (int i = 0; i < 10; i++)
        {
            //�X�R�A��\��
            rankingNames[i].text = getScoreInfo[i].name;
            rankingScores[i].text = getScoreInfo[i].score.ToString();
            /*
            if(scoreInfo.name != "")
            {
                //�X�R�A��\��
                rankingNames[i].text = scoreInfo.name;
                rankingScores[i].text = scoreInfo.score.ToString();
            }
            */
        }
        //���X�g�̒��g���������
        Scorelist.Clear();
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
