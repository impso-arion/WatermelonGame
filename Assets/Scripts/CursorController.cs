using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CursorController : MonoBehaviour
{
    // GameDirector インスタンスを格納するための変数
    private GameDirector gameDirector;
    //マウスカーソル追いかけ用、座標用の変数
    Vector3 mousePos, worldPos, newPos;
    //ボール保持中かどうかのフラグ
    private bool ishang = false; // 初期値は false

    //Nectdirectorのインスタンスを格納するための変数
    private Nextdirector nextdirector;
    
    //連続操作を禁止するためのウェイトタイム
    private float lastMouseUpTime;
    public float waitTime = 0.1f;
    private bool isFirstClick = true;//最初のクリックではウェイトタイム不要です
    private float elapsedTime;//ウェイトタイム



    // Start is called before the first frame update
    void Start()
    {
        //ishang = false;//初期値はfalse

        //Nextdirectorのインスタンスを取得、nextBallフラグのために
        nextdirector = FindObjectOfType<Nextdirector>();
         // GameDirector インスタンスを取得。クリアフラグのために
        gameDirector = FindObjectOfType<GameDirector>();
        lastMouseUpTime = -100;//最初は落とせる
    }

    // Update is called once per frame
    void Update()
    {
        //クリアしているなら何もしない
        if (gameDirector != null)
        {
            // ゲームディレクターのクリアフラグを確認
            bool isGameClear = gameDirector.IsClear;

            // クリアフラグに応じた処理をここに記述
            if (isGameClear)
            {
                // ゲームがクリアした場合の処理
                //Debug.Log("クリアしてるならなにもしない");
                //手元のHangBallがあれば消す
                GameObject hangdball = GameObject.Find("HangBall");

                Destroy(hangdball);
            }
            else
            {
                // ゲームがクリアしていない場合の処理
                //elapsedTimeによる点線の表示


                // 一定時間が経過しているか確認
                elapsedTime = Time.time - lastMouseUpTime;
                //最初のクリック前であれば表示

                if (elapsedTime < waitTime)
                {
                    // ボタンが離されてから一定時間経過していないので
                    //点線を非表示
                    // オブジェクトを非アクティブにする
                    //Debug.Log("非アクティブにしたい");
                    // 子オブジェクトの名前を指定して検索
                    Transform guideTransform = transform.Find("guide");

                    if (guideTransform != null)
                    {
                        //guideのspriteRendererをとる
                        SpriteRenderer guideSpriteRenderer = guideTransform.GetComponent<SpriteRenderer>();
                        // 取得できたら何かしらの操作を行う
                        if (guideSpriteRenderer != null)
                        {
                            // ここで guideSpriteRenderer を使用する
                            guideSpriteRenderer.enabled = false;
                        }
                    }
                }
                else if (elapsedTime >= waitTime)//点線を表示
                {
                    // オブジェクトをアクティブにする
                    //Debug.Log("アクティブにしたい");
                    // 子オブジェクトの名前を指定して検索
                    Transform guideTransform = transform.Find("guide");

                    if (guideTransform != null)
                    {
                        //guideのspriteRendererをとる
                        SpriteRenderer guideSpriteRenderer = guideTransform.GetComponent<SpriteRenderer>();
                        // 取得できたら何かしらの操作を行う
                        if (guideSpriteRenderer != null)
                        {
                            // ここで guideSpriteRenderer を使用する
                            guideSpriteRenderer.enabled = true;
                        }
                    }
                }
                

                //マウスカーソルについていくが、Y方向は変わらない。箱からも出ない
                //マウス座標の取得
                mousePos = Input.mousePosition;
                //現在の位置をnewPosに
                newPos = transform.position;
                //スクリーン座標をワールド座標に変換
                worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                //ワールド座標を自身の座標に設定したいが、Y座標は変わらない
                newPos.x = worldPos.x;
                if(newPos.x > 3.8)//xは-2から4の間です
                {
                    newPos.x = 3.8f;
                }else if(newPos.x < -1.8)
                {
                    newPos.x = -1.8f;
                }
                transform.position = newPos;//カーソルのポジションはここ


                //ボールを持っていないならボールを持つ。
                if (!ishang )
                {
                //NextAreaに"NextBall"というオブジェクトがあったら
                //rigidbodyとcolliderを無効にするためのフラグを変える

                //矢印の直下に移動して"hangdBall"という名に変更しNextdirectorのフラグ
                //nextball = false; // ネクストボール有無をfalseにする
                //ishang = true;//フラグをtrueにする

                GameObject nextBallObject = GameObject.Find("NextBall");
                if (nextBallObject != null)
                {
                //Debug.Log("NextBallがありました");
                //rigidbodyとcolliderを無効にする
                //NextBallのフラグのあるスクリプトコンポーネントを取得
                BallCotroller BallScript = nextBallObject.GetComponent<BallCotroller>();
                
                if (BallScript != null)
                {
                    BallScript.SetIsHangd(true);//フラグをtrueにするとコンポーネントがはずれる
                    
                }
                nextBallObject.name = "HangBall";//ボール名称変更
                GameObject hangdball = GameObject.Find("HangBall");
                //hangdballの位置を出す
                if (hangdball != null)
                {
                    //カーソルの下に移動
                    Vector3 hangpos = newPos + new Vector3(0f, -0.8f, 0f); //少し下にオフセット
                    hangdball.transform.position = hangpos;
                            
                }
                if (nextdirector != null)
                {
                    //nextdirectorのnextBallフラグを変更
                    nextdirector.nextball = false;  
                }

                ishang = true;//フラグをtrueにする
                }
                else
                {
                    Debug.LogError("NextBallが見つかりません");
                }
                }
                else//ボールを持っているならば、ボールは追従する
                {
                    GameObject hangdball = GameObject.Find("HangBall");
                    //hangdballの位置を出す
                    if (hangdball != null)
                    {
                        //追従移動
                        Vector3 hangpos = newPos + new Vector3(0f, -0.8f, 0f); //少し下にオフセット
                        hangdball.transform.position = hangpos;
                    }
                }
                //ボールを落とす。hangballが存在するときだけ
                if (Input.GetMouseButtonUp(0) && ishang == true)
                {
                    if (worldPos.x >= -1.8f && worldPos.x <= 3.8f)
                    {
                        if (isFirstClick)
                        {
                            // 最初のクリック時は条件を考慮せずにボールを落とす
                            falls();
                            isFirstClick = false;
                            // 最後にボタンが離された時間を更新
                            lastMouseUpTime = Time.time;
                        }
                        else
                        {
                            
                            // 一定時間が経過しているか確認
                            if (elapsedTime >= waitTime)
                            {
                                // ボタンが離されてから一定時間が経過したのでボールを落とす
                                falls();

                                // 最後にボタンが離された時間を更新
                                lastMouseUpTime = Time.time;
                            }
                           

                        }
                    }

                }
            }
        }
        //ボールの処理
        //ボール保持中でなければnextBallを取得
    }

    private void falls()//落下
    {
        //Debug.Log("クリックされました");
        GameObject entball = GameObject.Find("HangBall");
        if (entball != null)
        {
            //rigidbodyとcolliderを有効にする
            //NextBallのフラグのあるスクリプトコンポーネントを取得
            BallCotroller BallScript = entball.GetComponent<BallCotroller>();
            if (BallScript != null)
            {
                BallScript.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
            }
            //entityとなるボールのIDを取得して名前に反映する。
            int BallID = BallScript.ballID;
            entball.name = "entityBall_" + BallID.ToString();//名前を変更する

            //ボックスに入ったボールのballIDを、GameDirectorのリストに追加する。
            // gameDirectorオブジェクトを取得
            GameDirector gameDirector = FindObjectOfType<GameDirector>();

            // BallIDを保存
            gameDirector.ballList.Add(BallID);
        }
        ishang = false;



        //ball保持中でなければなにもしない
        //ball保持しているなら、持っているボールを落とす
    }


}
