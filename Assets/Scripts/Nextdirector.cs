using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextdirector : MonoBehaviour
{
    // GameDirector �C���X�^���X���i�[���邽�߂̕ϐ�
    private GameDirector gameDirector;
    public bool nextball = false; // �l�N�X�g�{�[���L���B�����l��false


    public GameObject[] balls; // Ball1 ���� Ball6 �܂ł̃v���n�u���i�[����z��
    public Vector3 spawnPoint; // �I�u�W�F�N�g�̐����ʒu�B��ʂŕς�����
    //public string prefabPath; // �v���n�u�̃A�Z�b�g�p�X�i�t�H���_���̃v���n�u�ւ̃p�X�j


    // Start is called before the first frame update
    void Start()
    {
        // GameDirector �C���X�^���X���擾�B�N���A�t���O�̂��߂�
        gameDirector = FindObjectOfType<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameDirector != null)
        {
            // �Q�[���f�B���N�^�[�̃N���A�t���O���m�F
            bool isGameClear = gameDirector.IsClear;

            // �N���A�t���O�ɉ����������������ɋL�q
            if (isGameClear)
            {
                // �Q�[�����N���A�����ꍇ�̏���
                //Debug.Log("�N���A���Ă�Ȃ�Ȃɂ����Ȃ�");
                return;
            }
            else
            {
                // �Q�[�����N���A���Ă��Ȃ��ꍇ�̏���
                //�{�[���\�����s���Ă��邩���`�F�b�N
                //Debug.Log("�N���A���Ă��Ȃ��̂Ń{�[���L����");
                if (!nextball)
                {
                    //Debug.Log("�{�[���Ȃ��̂Ŕ���������");
                    //�{�[�����o��������
                    // �����_���ȃC���f�b�N�X�𐶐�
                    int randomIndex = Random.Range(0, balls.Length);

                    // �����_���ȃI�u�W�F�N�g�𐶐�
                    GameObject randomBall = Instantiate(balls[randomIndex], spawnPoint, Quaternion.identity);
                    // �V����ID���擾
                    int newBallID = BallIDManager.Instance.GetNewBallID();
                    
                    
                    //ID���{�[���ɓo�^
                    BallCotroller BallScript = randomBall.GetComponent<BallCotroller>();
                    BallScript.ballID = newBallID;
                    randomBall.name = "NextBall";//�l�N�X�g�{�[���̖��́Bfind�ŒT���o����
                    nextball = true;
                    
            }
            }
        }

    }
}
