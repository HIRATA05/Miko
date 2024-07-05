using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;
using static TestGameManager;
using System;

public class GSSA_ScoreManager : MonoBehaviour
{
    [SerializeField] private TestGameManager testGameManager;
    

    void Start()
    {

    }

    void Update()
    {

    }

    //�f�[�^�̕ۑ��֐�
    public void ChatLogSave(string name, int score)
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = name;
        so["message"] = score;
        so.SaveAsync();
    }

    public void DataUpdate(string name, int score)
    {
        //�f�[�^�̍X�V�R���[�`�����Ă�
        StartCoroutine(ChatLogUpdateIterator(name, score));
    }
    //�f�[�^�̍X�V�R���[�`��
    private IEnumerator ChatLogUpdateIterator(string name, int score)
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", name);
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log("�f�[�^�X�V�F" + so["name"] + ">" + so["message"]);
            so["message"] = score;
            yield return so.SaveAsync();
        }
    }

    public void DataGet()
    {
        //�f�[�^�̎擾�R���[�`�����Ă�
        StartCoroutine(ChatLogGetIterator());
    }
    public IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        //query.Where("name", "=", "���[��");
        yield return query.FindAsync();

        foreach (var so in query.Result)
        {
            Debug.Log(so["name"] + ">" + so["message"]);

            string s = so["message"].ToString();

            //string,int�ϊ����ă��X�g�ɉ�����
            testGameManager.Scorelist.Add(new ScoreInfo { name = so["name"].ToString(), score = int.Parse(s) });
        }
        //�X�R�A�����ւ���
        testGameManager.Scorelist.Sort((a, b) => b.score - a.score);
        
        //Debug.Log("�v�f���F" + testGameManager.Scorelist.Count);

        //10�ʕ�getScoreInfo�ɓ����
        for (int i = 0; i < 10; i++)
        {
            //���X�g�͈͓̔����m�F
            if((i >= 0) && (i < testGameManager.Scorelist.Count))
            {
                testGameManager.getScoreInfo[i].score = testGameManager.Scorelist[i].score;
                testGameManager.getScoreInfo[i].name = testGameManager.Scorelist[i].name;
            }
            else
            {
                testGameManager.getScoreInfo[i] = new ScoreInfo("", 0);
            }
            Debug.Log("�擾�������X�g�F" + testGameManager.getScoreInfo[i].name);
        }
    }

    public void DataDelete(string name)
    {
        //�f�[�^�̏����R���[�`�����Ă�
        StartCoroutine(ChatLogDelete(name));
    }
    //�f�[�^�����֐�
    private IEnumerator ChatLogDelete(string name)
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", name);
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log("�f�[�^�����F" + so["name"] + ">" + so["message"]);
            so["name"] = "";
            so["message"] = "";
            yield return so.SaveAsync();
        }
    }
}
