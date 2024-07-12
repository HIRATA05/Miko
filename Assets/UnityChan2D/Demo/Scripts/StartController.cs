﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class StartController : MonoBehaviour
{
    [SceneName]
    public string nextLevel;

    [SerializeField]
    private KeyCode enter = KeyCode.X;

    [SerializeField]
    private ScoreManager scoreManager;

    void Start()
    {
        //スコアの初期化
        scoreManager.ScoreInit(scoreManager.scoreData);
    }

    void Update()
    {
        if (Input.GetKeyDown(enter) && scoreManager.scoreData.Name != "")
        {
            StartCoroutine(LoadStage());
        }
    }

    private IEnumerator LoadStage()
    {
        foreach (AudioSource audioS in FindObjectsOfType<AudioSource>())
        {
            audioS.volume = 0.2f;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;

        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        SceneManager.LoadScene(nextLevel);
    }
}
