using System.Collections;
using System.Linq;
using UnityEngine;

using GSSA;
public class GSSATest : MonoBehaviour
{
    string playerName = "";

    int score = 100;

    void Start()
    {
        
    }

    //�f�[�^�̕ۑ��֐�
    public void ChatLogSave()
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = "playerName";
        so["message"] = "���ׂȂ��ł��������I";
        so.SaveAsync();
    }

    public void DataUpdate()
    {
        //�f�[�^�̍X�V�R���[�`�����Ă�
        StartCoroutine(ChatLogGetIterator());
    }
    //�f�[�^�̍X�V�R���[�`��
    private IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", "shouta");
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log(so["name"] + ">" + so["message"]);
            so["message"] = score;
            yield return so.SaveAsync();
        }
    }
}