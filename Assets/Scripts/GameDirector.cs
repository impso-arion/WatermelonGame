using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;
using Unity.VisualScripting;

public class GameDirector : MonoBehaviour
{

    public GameObject prefabCollisionEx;//�Փˎ����o
    public bool isClear = false; // �N���A�t���O�̏����l�� false
    public List<int> ballList = new List<int>();//�{�[���̃��X�g�B
    // �Q�[���N���A���ɃN���A�t���O�� true �ɐݒ�
    
    //��������{�[���v���n�u�̐錾�B�A�^�b�`���Ă���������
    public GameObject ball2;
    public GameObject ball3;
    public GameObject ball4;
    public GameObject ball5;
    public GameObject ball6;
    public GameObject ball7;
    public GameObject ball8;
    public GameObject ball9;
    public GameObject ball10;
    public GameObject ball11;

    GameObject txtScore;
    int score = 0;

    GameObject gameover;

    //BGM
    private AudioSource audioSource;
    //�G�t�F�N�g����
    private AudioSource audio_re;
    private AudioSource audio_mi;
    private AudioSource audio_fa;
    private AudioSource audio_so;
    private AudioSource audio_ra;
    private AudioSource audio_si;
    private AudioSource audio_do;
    //�Q�[���I�[�o�[����
    private AudioSource audio_gameover;


    public void GameClear()
    {
        isClear = true;
    }
    // �N���A�t���O���O������擾���邽�߂̃v���p�e�B���쐬
    public bool IsClear
    {
        get { return isClear; }
    }

    // �N���A�������������ꂽ�ۂ�isClear��true�ɐݒ肷�郁�\�b�h
    public void SetGameClear()
    {
        Debug.Log("�N���A����");
        this.gameover.GetComponent<SpriteRenderer>().enabled = true;
        audioSource.Stop();
        audio_gameover.Play();
        isClear = true;
    }
    



    // Start is called before the first frame update
    void Start()
    {
        //FPS�̎w��
        Application.targetFrameRate = 60;
        isClear = false;//�ŏ��̓N���A���Ă��Ȃ�

        //BallIDManager

        //text
        this.txtScore = GameObject.Find("txtScore");
        this.gameover = GameObject.Find("GAMEOVER");

        //�����擾
        // �q�I�u�W�F�N�g�̖��O���w�肵�� AudioSource �R���|�[�l���g���擾
        audio_re = transform.Find("soundRe").GetComponent<AudioSource>();
        audio_mi = transform.Find("soundMi").GetComponent<AudioSource>();
        audio_fa = transform.Find("soundFa").GetComponent<AudioSource>();
        audio_so = transform.Find("soundSo").GetComponent<AudioSource>();
        audio_ra = transform.Find("soundRa").GetComponent<AudioSource>();
        audio_si = transform.Find("soundSi").GetComponent<AudioSource>();
        audio_do = transform.Find("soundDo").GetComponent<AudioSource>();

        audio_gameover = transform.Find("GameOverLine").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        AudioListener.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        this.txtScore.GetComponent<TextMeshProUGUI>().text = score.ToString();

        //���g���C���������ꂽ���蒼����
    }
    /// <summary>
    /// �y�A�Ȃ�Ώ�����
    /// </summary>
    /// <param name="collisionPoint"></param>�Փ˓_
    /// <param name="TagName"></param>���^�O�Ȃ�Ώ�����
    /// <param name="ballID"></param>�����̃{�[��ID
    /// <param name="colballID"></param>����̃{�[��ID
    public void pairchk(Vector2 collisionPoint, string TagName,int ballID, int colballID)
    {
        //�����T���BballList�ɑ��肪���݂���Ȃ�΁A�����ƈꏏ�ɏ�����B
        //ballList�ɑ��肪���݂��Ȃ���΂Ȃɂ����Ȃ��B
        if (ballList.Contains(colballID))
        {
            //ؽē��ɑ��݂���
            //Debug.Log("����݂�������");
            //���X�g������ballID��colballID���폜
            ballList.Remove(ballID);
            ballList.Remove(colballID);
            //
            //�����I�u�W�F�N�g�̍폜
            GameObject myball = GameObject.Find("entityBall_" + ballID.ToString());
            // �I�u�W�F�N�g�������������m�F���Ă���j��
            if (myball != null)
            {
                Destroy(myball);
            }
            else
            {
                Debug.LogWarning("myball��������܂���ł����B");
            }
            //����I�u�W�F�N�g�̍폜
            GameObject colball = GameObject.Find("entityBall_" + colballID.ToString());
            // �I�u�W�F�N�g�������������m�F���Ă���j��
            if (colball != null)
            {
                Destroy(colball);
            }
            else
            {
                Debug.LogWarning("colballl��������܂���ł����B");
            }
            //���������Ƃɍ��̂���
            MergeNext(collisionPoint, TagName);
        }
        else
        {
            //Debug.Log("�Ȃ��݂�������");
            //ؽē��ɑ��݂��Ȃ��Ȃ�΂Ȃɂ����Ȃ�
        }
    }



    /// <summary>
    /// ���̃{�[���𐶐�����
    /// </summary>
    /// <param name="collisionPoint"></param>
    /// <param name="TagName"></param>

    public void MergeNext(Vector2 collisionPoint, string TagName)
    {
        //entity�ƂȂ�{�[����ID���擾
        int BallID = BallIDManager.Instance.GetNewBallID();
        // �󂯎����������������
        //Debug.Log("Received coordinates: " + collisionPoint);
        //Debug.Log("Received tagName: " + TagName);
        // Vector2����Vector3�ɕϊ�
        Vector3 position3D = new Vector3(collisionPoint.x, collisionPoint.y, 0f);

        switch (TagName)
        {
            case "1cherry":
                score += 1;
                GameObject newObj1 = Instantiate(ball2, position3D, Quaternion.identity);
                BallCotroller BallScript = newObj1.GetComponent<BallCotroller>();
                if (BallScript != null)
                {
                    BallScript.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript.ballID = BallID;
                }                
                newObj1.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);

                //newObj1.name = "entityBall"; // �I�u�W�F�N�g�̖��O��ݒ�
                //���o���ƃp�[�e�B�N��
                audio_re.Play();
                GameObject obj1 = Instantiate(prefabCollisionEx,position3D,Quaternion.identity);
                Destroy(obj1, 1.5f);//�p�[�e�B�N������
                break;
            case "2strawberry":
                score += 3;
                GameObject newObj2 = Instantiate(ball3, position3D, Quaternion.identity);
                BallCotroller BallScript1 = newObj2.GetComponent<BallCotroller>();
                if (BallScript1 != null)
                {
                    BallScript1.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript1.ballID = BallID;
                }
                newObj2.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_re.Play();
                GameObject obj2 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj2, 1.5f);//�p�[�e�B�N������
                break;
            case "3grape":
                score += 6;
                GameObject newObj3 = Instantiate(ball4, position3D, Quaternion.identity);
                BallCotroller BallScript2 = newObj3.GetComponent<BallCotroller>();
                if (BallScript2 != null)
                {
                    BallScript2.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript2.ballID = BallID;
                }
                
                BallScript2.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_mi.Play();
                GameObject obj3 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj3, 1.5f);//�p�[�e�B�N������
                break;
            case "4orange":
                score += 10;
                GameObject newObj4 = Instantiate(ball5, position3D, Quaternion.identity);
                BallCotroller BallScript3 = newObj4.GetComponent<BallCotroller>();
                if (BallScript3 != null)
                {
                    BallScript3.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript3.ballID = BallID;
                }
                
                BallScript3.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_fa.Play();
                GameObject obj4 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj4, 1.5f);//�p�[�e�B�N������
                break;
            case "5persimmon":
                score += 15;
                GameObject newObj5 = Instantiate(ball6, position3D, Quaternion.identity);
                BallCotroller BallScript4 = newObj5.GetComponent<BallCotroller>();
                if (BallScript4 != null)
                {
                    BallScript4.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript4.ballID = BallID;
                }
                
                BallScript4.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_so.Play();
                GameObject obj5 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj5, 1.5f);//�p�[�e�B�N������
                break;
            case "6apple":
                score += 21;
                GameObject newObj6 = Instantiate(ball7, position3D, Quaternion.identity);
                BallCotroller BallScript5 = newObj6.GetComponent<BallCotroller>();
                if (BallScript5 != null)
                {
                    BallScript5.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript5.ballID = BallID;
                }
                BallScript5.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_ra.Play();
                GameObject obj6 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj6, 1.5f);//�p�[�e�B�N������
                break;
            case "7pear":
                score += 28;
                GameObject newObj7 = Instantiate(ball8, position3D, Quaternion.identity);
                BallCotroller BallScript6 = newObj7.GetComponent<BallCotroller>();
                if (BallScript6 != null)
                {
                    BallScript6.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript6.ballID = BallID;
                }
                
                BallScript6.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_si.Play();
                GameObject obj7 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj7, 1.5f);//�p�[�e�B�N������
                break;
            case "8peach":
                score += 36;
                GameObject newObj8 = Instantiate(ball9, position3D, Quaternion.identity);
                BallCotroller BallScript7 = newObj8.GetComponent<BallCotroller>();
                if (BallScript7 != null)
                {
                    BallScript7.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript7.ballID = BallID;
                }
                
                BallScript7.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj8 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj8, 1.5f);//�p�[�e�B�N������
                break;
            case "9pineapple":
                score += 45;
                GameObject newObj9 = Instantiate(ball10, position3D, Quaternion.identity);
                BallCotroller BallScript8 = newObj9.GetComponent<BallCotroller>();
                if (BallScript8 != null)
                {
                    BallScript8.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript8.ballID = BallID;
                }
                
                BallScript8.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj9 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj9, 1.5f);//�p�[�e�B�N������
                break;
            case "10melon":
                score += 55;
                GameObject newObj10 = Instantiate(ball11, position3D, Quaternion.identity);
                BallCotroller BallScript9 = newObj10.GetComponent<BallCotroller>();
                if (BallScript9 != null)
                {
                    BallScript9.SetIsHangd(false);//�t���O��false�ɂ���ƃR���|�[�l���g����
                    BallScript9.ballID = BallID;
                }
                
                BallScript9.name = "entityBall_" + BallID.ToString();//���O��ύX����
                // BallID��ۑ�
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj10 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj10, 1.5f);//�p�[�e�B�N������
                break;
            case "11watermelon":
                score += 66;
                //Instantiate(ball6, position3D, Quaternion.identity);
                audio_do.Play();
                GameObject obj11 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj11, 1.5f);//�p�[�e�B�N������
                break;
            default: break;
        }
    }







}
