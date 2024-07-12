using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;
using static ScoreManager;
using System;

public class GSSAWorld : MonoBehaviour
{
    //データの保存関数
    public void ChatLogSave(string name, int score)
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = name;
        so["message"] = score;
        so.SaveAsync();
    }
}
