using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCotroller : MonoBehaviour
{
    // GameDirector �C���X�^���X���i�[���邽�߂̕ϐ�
    private GameDirector gameDirector;
    //isHangd�t���O�L���ŃR���|�[�l���g���I���I�t
    private Rigidbody2D _rb;
    private CircleCollider2D _cc2D1;//
    //private CircleCollider2D _cc2D2;//
    private bool isHangd = false;
    string myTag = "";

    private bool isEntity = false;//�o�ꂵ���{�[�����ǂ����B�Q�[���I�[�o�[����ɂ���B

    //���̂������ǂ���
    //public bool isMergeFlag = false;
    
    public int ballID = 0;

    // Start is called before the first frame update
    void Start()
    {
        // GameDirector �C���X�^���X���擾�B�N���A�t���O�̂��߂�
        gameDirector = FindObjectOfType<GameDirector>();

        _rb = GetComponent<Rigidbody2D>();
        _cc2D1 = GetComponent<CircleCollider2D>();
        // �Q�[���I�u�W�F�N�g�̃^�O���擾
        myTag = gameObject.tag;

        //�{�[���̃C���X�^���XID���擾���ĉ���
        //ballID = GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        // �t���O�Ɋ�Â���isKinematic�v���p�e�B��ݒ�
        _rb.isKinematic = isHangd;
        _cc2D1.enabled = !isHangd;

        //isEntity�t���O��true�ň��̏ꏊ�ȏゾ������N���A(�Q�[���I�[�o�[)
        if(isEntity == true && transform.position.y > 2.6)
        {
            if (gameDirector != null)
            {
                gameDirector.SetGameClear();
                //gameDirector.isClear = true;//�N���A�ɂ��邱�ƂŁA�l�N�X�g���������Ȃ��Ȃ�
                //��x�����Ăяo���΂悢�̂ŁA�t���O��߂�
                isEntity = false;
            }
        }
    }

    // �O������isHangd�t���O��ݒ肷�邽�߂̃��\�b�h
    public void SetIsHangd(bool value)
    {
        isHangd = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)//�Փ˂����ꍇ�̓���
    {
        //�Փˑ���
        GameObject colobj = collision.gameObject;
        //�Փˏꏊ
        Vector2 collisionPoint;
        // �Փ˂�������̃I�u�W�F�N�g���� "NextGrand" �ł��邩���m�F
        if (collision.gameObject.name == "NextGrand")
        {
            // "NextGrand" �I�u�W�F�N�g��ǂ������ꍇ�͏������X�L�b�v���ďI��
            return;
        }
        if (collision.gameObject.name == "kabe1")
        {
            return;
        }
        if (collision.gameObject.name == "kabe2")
        {
            return;
        }
        
        isEntity = true;

        // �Փ˂�������̃Q�[���I�u�W�F�N�g�������^�O�������Ă��邩�m�F
        if (colobj.CompareTag(myTag))
        {
            int colballID = 0; 
            // �����ƏՓ˂������肪�����^�O�����ꍇ�̏����������ɋL�q
            //Debug.Log("�����^�O�̃I�u�W�F�N�g���m���Փ˂��܂����I");

            // �Փ˓_�̔z����擾
            ContactPoint2D[] contacts = collision.contacts;

                // �ŏ��̏Փ˓_���擾
                if (contacts.Length > 0)
                {
                    collisionPoint = contacts[0].point;
                    // collisionPoint �ɏՓ˂����ʒu���i�[����܂�
                
                
                // �Փ˂����ʒu�𕶎���ɕϊ�
                string collisionPointString = "Collision Point: " + collisionPoint.ToString();

                // Debug.Log �ŕ\��
                Debug.Log(collisionPointString);
                Debug.Log(myTag);

                BallCotroller collisionScript = colobj.GetComponent<BallCotroller>();
                if (collisionScript != null)
                {
                    colballID = collisionScript.ballID;
                    Debug.Log("������" + ballID + "�����" + colballID);
                }
                GameDirector receiver = FindObjectOfType<GameDirector>(); // ��M���̃X�N���v�g��������
                if (receiver != null)
                {
                    receiver.pairchk(collisionPoint, myTag, ballID, colballID);
                }
            }
        }
    }
}
