using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
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



    //�����L���O��ʂŎg�p����ϐ�---
    //�����L���O�ŕ\������e�L�X�g�̔z��
    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];

    //�����L���O�\���̑ҋ@���
    [SerializeField] private GameObject WaitPanal;

    //�����L���O�\���̃t���O
    bool RankingDisplay = false;


    [Header("�X�v���b�h�V�[�g�̃X�R�A�Ǘ��X�N���v�g")]
    [SerializeField] private SSManager ssManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�X�R�A���擾���Ă��Ȃ����V�[�g����擾����
        if (!RankingDisplay)
        {
            RankingDisplay = true;

            //�R���[�`�����g���ă����L���O��\�� �����L���O��ʂł�����g��
            StartCoroutine(DisplayRanking());
        }
    }

    private IEnumerator DisplayRanking()
    {
        //�X�R�A�̎擾���I������܂őҋ@����
        yield return ssManager.ChatLogGetIterator();
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


}
