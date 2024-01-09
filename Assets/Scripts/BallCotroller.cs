using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCotroller : MonoBehaviour
{
    // GameDirector インスタンスを格納するための変数
    private GameDirector gameDirector;
    //isHangdフラグ有無でコンポーネントをオンオフ
    private Rigidbody2D _rb;
    private CircleCollider2D _cc2D1;//
    //private CircleCollider2D _cc2D2;//
    private bool isHangd = false;
    string myTag = "";

    private bool isEntity = false;//登場したボールかどうか。ゲームオーバー判定にする。

    //合体したかどうか
    //public bool isMergeFlag = false;
    
    public int ballID = 0;

    // Start is called before the first frame update
    void Start()
    {
        // GameDirector インスタンスを取得。クリアフラグのために
        gameDirector = FindObjectOfType<GameDirector>();

        _rb = GetComponent<Rigidbody2D>();
        _cc2D1 = GetComponent<CircleCollider2D>();
        // ゲームオブジェクトのタグを取得
        myTag = gameObject.tag;

        //ボールのインスタンスIDを取得して可視化
        //ballID = GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        // フラグに基づいてisKinematicプロパティを設定
        _rb.isKinematic = isHangd;
        _cc2D1.enabled = !isHangd;

        //isEntityフラグがtrueで一定の場所以上だったらクリア(ゲームオーバー)
        if(isEntity == true && transform.position.y > 2.6)
        {
            if (gameDirector != null)
            {
                gameDirector.SetGameClear();
                //gameDirector.isClear = true;//クリアにすることで、ネクストが発生しなくなる
                //一度だけ呼び出せばよいので、フラグを戻す
                isEntity = false;
            }
        }
    }

    // 外部からisHangdフラグを設定するためのメソッド
    public void SetIsHangd(bool value)
    {
        isHangd = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)//衝突した場合の動作
    {
        //衝突相手
        GameObject colobj = collision.gameObject;
        //衝突場所
        Vector2 collisionPoint;
        // 衝突した相手のオブジェクト名が "NextGrand" であるかを確認
        if (collision.gameObject.name == "NextGrand")
        {
            // "NextGrand" オブジェクトや壁だった場合は処理をスキップして終了
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

        // 衝突した相手のゲームオブジェクトが同じタグを持っているか確認
        if (colobj.CompareTag(myTag))
        {
            int colballID = 0; 
            // 自分と衝突した相手が同じタグを持つ場合の処理をここに記述
            //Debug.Log("同じタグのオブジェクト同士が衝突しました！");

            // 衝突点の配列を取得
            ContactPoint2D[] contacts = collision.contacts;

                // 最初の衝突点を取得
                if (contacts.Length > 0)
                {
                    collisionPoint = contacts[0].point;
                    // collisionPoint に衝突した位置が格納されます
                
                
                // 衝突した位置を文字列に変換
                string collisionPointString = "Collision Point: " + collisionPoint.ToString();

                // Debug.Log で表示
                Debug.Log(collisionPointString);
                Debug.Log(myTag);

                BallCotroller collisionScript = colobj.GetComponent<BallCotroller>();
                if (collisionScript != null)
                {
                    colballID = collisionScript.ballID;
                    Debug.Log("自分は" + ballID + "相手は" + colballID);
                }
                GameDirector receiver = FindObjectOfType<GameDirector>(); // 受信側のスクリプトを見つける
                if (receiver != null)
                {
                    receiver.pairchk(collisionPoint, myTag, ballID, colballID);
                }
            }
        }
    }
}
