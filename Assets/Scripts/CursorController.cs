using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CursorController : MonoBehaviour
{
    // GameDirector �C���X�^���X���i�[���邽�߂̕ϐ�
    private GameDirector gameDirector;
    //�}�E�X�J�[�\���ǂ������p�A���W�p�̕ϐ�
    Vector3 mousePos, worldPos, newPos;
    //�{�[���ێ������ǂ����̃t���O
    private bool ishang = false; // �����l�� false

    //Nectdirector�̃C���X�^���X���i�[���邽�߂̕ϐ�
    private Nextdirector nextdirector;
    
    //�A��������֎~���邽�߂̃E�F�C�g�^�C��
    private float lastMouseUpTime;
    public float waitTime = 0.1f;
    private bool isFirstClick = true;//�ŏ��̃N���b�N�ł̓E�F�C�g�^�C���s�v�ł�
    private float elapsedTime;//�E�F�C�g�^�C��



    // Start is called before the first frame update
    void Start()
    {
        //ishang = false;//�����l��false

        //Nextdirector�̃C���X�^���X���擾�AnextBall�t���O�̂��߂�
        nextdirector = FindObjectOfType<Nextdirector>();
         // GameDirector �C���X�^���X���擾�B�N���A�t���O�̂��߂�
        gameDirector = FindObjectOfType<GameDirector>();
        lastMouseUpTime = -100;//�ŏ��͗��Ƃ���
    }

    // Update is called once per frame
    void Update()
    {
        //�N���A���Ă���Ȃ牽�����Ȃ�
        if (gameDirector != null)
        {
            // �Q�[���f�B���N�^�[�̃N���A�t���O���m�F
            bool isGameClear = gameDirector.IsClear;

            // �N���A�t���O�ɉ����������������ɋL�q
            if (isGameClear)
            {
                // �Q�[�����N���A�����ꍇ�̏���
                //Debug.Log("�N���A���Ă�Ȃ�Ȃɂ����Ȃ�");
                //�茳��HangBall������Ώ���
                GameObject hangdball = GameObject.Find("HangBall");

                Destroy(hangdball);
            }
            else
            {
                // �Q�[�����N���A���Ă��Ȃ��ꍇ�̏���
                //elapsedTime�ɂ��_���̕\��


                // ��莞�Ԃ��o�߂��Ă��邩�m�F
                elapsedTime = Time.time - lastMouseUpTime;
                //�ŏ��̃N���b�N�O�ł���Ε\��

                if (elapsedTime < waitTime)
                {
                    // �{�^����������Ă����莞�Ԍo�߂��Ă��Ȃ��̂�
                    //�_�����\��
                    // �I�u�W�F�N�g���A�N�e�B�u�ɂ���
                    //Debug.Log("��A�N�e�B�u�ɂ�����");
                    // �q�I�u�W�F�N�g�̖��O���w�肵�Č���
                    Transform guideTransform = transform.Find("guide");

                    if (guideTransform != null)
                    {
                        //guide��spriteRenderer���Ƃ�
                        SpriteRenderer guideSpriteRenderer = guideTransform.GetComponent<SpriteRenderer>();
                        // �擾�ł����牽������̑�����s��
                        if (guideSpriteRenderer != null)
                        {
                            // ������ guideSpriteRenderer ���g�p����
                            guideSpriteRenderer.enabled = false;
                        }
                    }
                }
                else if (elapsedTime >= waitTime)//�_����\��
                {
                    // �I�u�W�F�N�g���A�N�e�B�u�ɂ���
                    //Debug.Log("�A�N�e�B�u�ɂ�����");
                    // �q�I�u�W�F�N�g�̖��O���w�肵�Č���
                    Transform guideTransform = transform.Find("guide");

                    if (guideTransform != null)
                    {
                        //guide��spriteRenderer���Ƃ�
                        SpriteRenderer guideSpriteRenderer = guideTransform.GetComponent<SpriteRenderer>();
                        // �擾�ł����牽������̑�����s��
                        if (guideSpriteRenderer != null)
                        {
                            // ������ guideSpriteRenderer ���g�p����
                            guideSpriteRenderer.enabled = true;
                        }
                    }
                }
                

                //�}�E�X�J�[�\���ɂ��Ă������AY�����͕ς��Ȃ��B��������o�Ȃ�
                //�}�E�X���W�̎擾
                mousePos = Input.mousePosition;
                //���݂̈ʒu��newPos��
                newPos = transform.position;
                //�X�N���[�����W�����[���h���W�ɕϊ�
                worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                //���[���h���W�����g�̍��W�ɐݒ肵�������AY���W�͕ς��Ȃ�
                newPos.x = worldPos.x;
                if(newPos.x > 3.8)//x��-2����4�̊Ԃł�
                {
                    newPos.x = 3.8f;
                }else if(newPos.x < -1.8)
                {
                    newPos.x = -1.8f;
                }
                transform.position = newPos;//�J�[�\���̃|�W�V�����͂���


                //�{�[���������Ă��Ȃ��Ȃ�{�[�������B
                if (!ishang )
                {
                //NextArea��"NextBall"�Ƃ����I�u�W�F�N�g����������
                //rigidbody��collider�𖳌��ɂ��邽�߂̃t���O��ς���

                //���̒����Ɉړ�����"hangdBall"�Ƃ������ɕύX��Nextdirector�̃t���O
                //nextball = false; // �l�N�X�g�{�[���L����false�ɂ���
                //ishang = true;//�t���O��true�ɂ���

                GameObject nextBallObject = GameObject.Find("NextBall");
                if (nextBallObject != null)
                {
                //Debug.Log("NextBall������܂���");
                //rigidbody��collider�𖳌��ɂ���
                //NextBall�̃t���O�̂���X�N���v�g�R���|�[�l���g���擾
                BallCotroller BallScript = nextBallObject.GetComponent<BallCotroller>();
                
                if (BallScript != null)
                {
                    BallScript.SetIsHangd(true);//�t���O��true�ɂ���ƃR���|�[�l���g���͂����
                    
                }
                nextBallObject.name = "HangBall";//�{�[�����̕ύX
                GameObject hangdball = GameObject.Find("HangBall");
                //hangdball�̈ʒu���o��
                if (hangdball != null)
                {
                    //�J�[�\���̉��Ɉړ�
                    Vector3 hangpos = newPos + new Vector3(0f, -0.8f, 0f); //�������ɃI�t�Z�b�g
                    hangdball.transform.position = hangpos;
                            
                }
                if (nextdirector != null)
                {
                    //nextdirector��nextBall�t���O��ύX
                    nextdirector.nextball = false;  
                }

                ishang = true;//�t���O��true�ɂ���
                }
                else
                {
                    Debug.LogError("NextBall��������܂���");
                }
                }
                else//�{�[���������Ă���Ȃ�΁A�{�[���͒Ǐ]����
                {
                    GameObject hangdball = GameObject.Find("HangBall");
                    //hangdball�̈ʒu���o��
                    if (hangdball != null)
                    {
                        //�Ǐ]�ړ�
                        Vector3 hangpos = newPos + new Vector3(0f, -0.8f, 0f); //�������ɃI�t�Z�b�g
                        hangdball.transform.position = hangpos;
                    }
                }
                //�{�[���𗎂Ƃ��Bhangball�����݂���Ƃ�����
                if (Input.GetMouseButtonUp(0) && ishang == true)
                {
                    if (worldPos.x >= -1.8f && worldPos.x <= 3.8f)
                    {
                        if (isFirstClick)
                        {
                            // �ŏ��̃N���b�N���͏������l�������Ƀ{�[���𗎂Ƃ�
                            falls();
                            isFirstClick = false;
                            // �Ō�Ƀ{�^���������ꂽ���Ԃ��X�V
                            lastMouseUpTime = Time.time;
                        }
                        else
                        {
                            
                            // ��莞�Ԃ��o�߂��Ă��邩�m�F
                            if (elapsedTime >= waitTime)
                            {
                                // �{�^����������Ă����莞�Ԃ��o�߂����̂Ń{�[���𗎂Ƃ�
                                falls();

                                // �Ō�Ƀ{�^���������ꂽ���Ԃ��X�V
                                lastMouseUpTime = Time.time;
                            }
                           

                        }
                    }

                }
            }
        }
        //�{�[���̏���
        //�{�[���ێ����łȂ����nextBall���擾
    }

    private void falls()//����
    {
        //Debug.Log("�N���b�N����܂���");
        GameObject entball = GameObject.Find("HangBall");
        if (entball != null)
        {
            //rigidbody��collider��L���ɂ���
            //NextBall�̃t���O�̂���X�N���v�g�R���|�[�l���g���擾
            BallCotroller BallScript = entball.GetComponent<BallCotroller>();
            if (BallScript != null)
            {
                BallScript.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
            }
            //entity�ƂȂ�{�[����ID���擾���Ė��O�ɔ��f����B
            int BallID = BallScript.ballID;
            entball.name = "entityBall_" + BallID.ToString();//���O��ύX����

            //�{�b�N�X�ɓ������{�[����ballID���AGameDirector�̃��X�g�ɒǉ�����B
            // gameDirector�I�u�W�F�N�g���擾
            GameDirector gameDirector = FindObjectOfType<GameDirector>();

            // BallID��ۑ�
            gameDirector.ballList.Add(BallID);
        }
        ishang = false;



        //ball�ێ����łȂ���΂Ȃɂ����Ȃ�
        //ball�ێ����Ă���Ȃ�A�����Ă���{�[���𗎂Ƃ�
    }


}
