using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;

public class GSSA_ScoreManager : MonoBehaviour
{
    string playerName = "";

    int score = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    //�f�[�^�̕ۑ��֐�
    public void ChatLogSave()
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = playerName;
        so["message"] = score;
        so.SaveAsync();
    }

    public void DataUpdate()
    {
        //�f�[�^�̍X�V�R���[�`�����Ă�
        StartCoroutine(ChatLogUpdateIterator());
    }
    //�f�[�^�̍X�V�R���[�`��
    private IEnumerator ChatLogUpdateIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", playerName);
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
    private IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        //query.Where("name", "=", "���[��");
        yield return query.FindAsync();

        foreach (var so in query.Result)
        {
            Debug.Log(so["name"] + ">" + so["message"]);
        }
    }

    public void DataDelete()
    {
        //�f�[�^�̏����R���[�`�����Ă�
        StartCoroutine(ChatLogDelete());
    }
    //�f�[�^�����֐�
    private IEnumerator ChatLogDelete()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", playerName);
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
