using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{

    //�^�C�g����ʂŎg�p����ϐ�---
    [Header("�v���C���[�����͂̃t�B�[���h")]
    [SerializeField] private InputField nameInputField;

    [Header("���O���͎��ɕ\������TEXT")]
    [SerializeField] private Text playerName;

    //�ʂ̃V�[���ł��X�R�A�Ǘ����ł���悤�ɃX�N���v�^�u����p��
    [Header("�X�R�A�̃X�N���v�^�u��")]
    [SerializeField] public ScoreData scoreData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���͂������O����ʂɕ\�����ăX�N���v�^�u���ɕۑ� �^�C�g����ʂ̖��O���͎��ɖ��O���̓t�B�[���h�Ɏg�� OnEndEdit
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;

        //scoreInfo.name = playerName.text;
        scoreData.Name = nameInputField.text;
    }
}
