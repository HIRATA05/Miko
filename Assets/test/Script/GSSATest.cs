using System.Collections;
using System.Linq;
using UnityEngine;

using GSSA;
public class GSSATest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChatLogGetIterator());
    }

    private IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", "かつーき");
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            so["message"] = "たべないよ！";
            yield return so.SaveAsync();
        }
    }
}