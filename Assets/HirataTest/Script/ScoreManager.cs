using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //�^�C�g���@�C���Q�[���@�����L���O�̂R�V�[���ŃA�^�b�` �֐����Ăяo���ăX�R�A���Ǘ�����


    /*
    //�e�X�g���Ɏg�p�����X�e�[�g ���ۂɑg�ݍ��ގ��̓R�����g�A�E�g�œ��삵�Ȃ��悤�ɂ��邱��
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
    */
    
    //�\���̂̒�`---
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
    
    //�V�[�g����擾����
    public ScoreInfo[] getScoreInfo = new ScoreInfo[10];

    //�X�R�A�\�[�g�p�̃��X�g���쐬
    public List<ScoreInfo> Scorelist = new List<ScoreInfo>();



    //�^�C�g����ʂŎg�p����ϐ�---
    [Header("�v���C���[�����͂̃t�B�[���h")]
    [SerializeField] private InputField nameInputField;

    [Header("���O���͎��ɕ\������TEXT")]
    [SerializeField] private Text playerName;


    /*
    //�����L���O��ʂ͌ʂ̃V�[�����쐬�������� ���ۂɑg�ݍ��ގ��̓R�����g�A�E�g�œ��삵�Ȃ��悤�ɂ��邱��
    //�����L���O��ʂŎg�p����ϐ�---
    //�����L���O�ŕ\������e�L�X�g�̔z��
    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];

    //�����L���O�\���̑ҋ@���
    [SerializeField] private GameObject WaitPanal;

    //�����L���O�\���̃t���O
    bool RankingDisplay = false;



    //�{�^���ŉ��Z�����X�R�A��\�� �e�X�g���Ɏg�p����
    [SerializeField] private Text ViewScore;
    */


    //�X�R�A���Ǘ����邽�߂Ɏg���F�X

    [Header("�X�v���b�h�V�[�g�̃X�R�A�Ǘ��X�N���v�g")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    //�ʂ̃V�[���ł��X�R�A�Ǘ����ł���悤�ɃX�N���v�^�u����p��
    [Header("�X�R�A�̃X�N���v�^�u��")]
    [SerializeField] public ScoreData scoreData;


    void Start()
    {
        //���ۂɑg�ݍ��ގ��̓R�����g�A�E�g�œ��삵�Ȃ��悤�ɂ��邱��
        /*
        ScoreInit(scoreData);
        */
    }

    
    void Update()
    {
        //���ۂɑg�ݍ��ގ��̓R�����g�A�E�g�œ��삵�Ȃ��悤�ɂ��邱��
        /*
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
                    gssa_Score.ChatLogSave(scoreData.Name, scoreData.Score);
                    //gssa_Score.ChatLogSave(scoreInfo.name, scoreInfo.score);

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

                    //�R���[�`�����g���ă����L���O��\�� �����L���O��ʂł�����g��
                    StartCoroutine(DisplayRanking());
                }

                break;
        }
        */
    }
    /*
    private IEnumerator DisplayRanking()
    {
        //�X�R�A�̎擾���I������܂őҋ@����
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
            
        }
        //���X�g�̒��g���������
        Scorelist.Clear();
    }
    */

    //���͂������O����ʂɕ\�����ăX�N���v�^�u���ɕۑ� �^�C�g����ʂ̖��O���͎��ɖ��O���̓t�B�[���h�Ɏg�� OnEndEdit
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;

        //scoreInfo.name = playerName.text;
        scoreData.Name = nameInputField.text;
    }

    //�������X�N���v�^�u���̃X�R�A�ɕۑ����� �Q�[���I�����̍ŏI�I�ȃX�R�A�����邱��
    public void ScoreSave(int score)
    {
        scoreData.Score = score;
    }

    //�X�N���v�^�u���̃X�R�A������������ �^�C�g����ʂ̃X�^�[�g�֐��Ŏg������
    public void ScoreInit(ScoreData scoreData)
    {
        scoreData.Name = "";
        scoreData.Score = 0;
    }

    /*
    //�X�R�A���Z�{�^�� �e�X�g�p�ɍ쐬��������
    public void ScoreAddButton()
    {
        //SCORE += 1;
        scoreData.Score += 1;
        //ViewScore.text = SCORE.ToString();
        ViewScore.text = scoreData.Score.ToString();
        //scoreInfo.score = SCORE;
        //scoreInfo.score = scoreData.Score;
    }
    */
}
