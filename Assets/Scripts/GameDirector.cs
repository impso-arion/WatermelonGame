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

    public GameObject prefabCollisionEx;//衝突時演出
    public bool isClear = false; // クリアフラグの初期値は false
    public List<int> ballList = new List<int>();//ボールのリスト。
    // ゲームクリア時にクリアフラグを true に設定
    
    //生成するボールプレハブの宣言。アタッチしてくださいね
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
    //エフェクト音源
    private AudioSource audio_re;
    private AudioSource audio_mi;
    private AudioSource audio_fa;
    private AudioSource audio_so;
    private AudioSource audio_ra;
    private AudioSource audio_si;
    private AudioSource audio_do;
    //ゲームオーバー音源
    private AudioSource audio_gameover;


    public void GameClear()
    {
        isClear = true;
    }
    // クリアフラグを外部から取得するためのプロパティを作成
    public bool IsClear
    {
        get { return isClear; }
    }

    // クリア条件が満たされた際にisClearをtrueに設定するメソッド
    public void SetGameClear()
    {
        Debug.Log("クリアした");
        this.gameover.GetComponent<SpriteRenderer>().enabled = true;
        audioSource.Stop();
        audio_gameover.Play();
        isClear = true;
    }
    



    // Start is called before the first frame update
    void Start()
    {
        //FPSの指定
        Application.targetFrameRate = 60;
        isClear = false;//最初はクリアしていない

        //BallIDManager

        //text
        this.txtScore = GameObject.Find("txtScore");
        this.gameover = GameObject.Find("GAMEOVER");

        //音源取得
        // 子オブジェクトの名前を指定して AudioSource コンポーネントを取得
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

        //リトライが押下されたらやり直せる
    }
    /// <summary>
    /// ペアならば消える
    /// </summary>
    /// <param name="collisionPoint"></param>衝突点
    /// <param name="TagName"></param>同タグならば消える
    /// <param name="ballID"></param>自分のボールID
    /// <param name="colballID"></param>相手のボールID
    public void pairchk(Vector2 collisionPoint, string TagName,int ballID, int colballID)
    {
        //相手を探す。ballListに相手が存在するならば、自分と一緒に消える。
        //ballListに相手が存在しなければなにもしない。
        if (ballList.Contains(colballID))
        {
            //ﾘｽﾄ内に存在する
            //Debug.Log("あるみたいだね");
            //リスト内からballIDとcolballIDを削除
            ballList.Remove(ballID);
            ballList.Remove(colballID);
            //
            //自分オブジェクトの削除
            GameObject myball = GameObject.Find("entityBall_" + ballID.ToString());
            // オブジェクトが見つかったか確認してから破棄
            if (myball != null)
            {
                Destroy(myball);
            }
            else
            {
                Debug.LogWarning("myballが見つかりませんでした。");
            }
            //相手オブジェクトの削除
            GameObject colball = GameObject.Find("entityBall_" + colballID.ToString());
            // オブジェクトが見つかったか確認してから破棄
            if (colball != null)
            {
                Destroy(colball);
            }
            else
            {
                Debug.LogWarning("colballlが見つかりませんでした。");
            }
            //消したあとに合体する
            MergeNext(collisionPoint, TagName);
        }
        else
        {
            //Debug.Log("ないみたいだね");
            //ﾘｽﾄ内に存在しないならばなにもしない
        }
    }



    /// <summary>
    /// 次のボールを生成する
    /// </summary>
    /// <param name="collisionPoint"></param>
    /// <param name="TagName"></param>

    public void MergeNext(Vector2 collisionPoint, string TagName)
    {
        //entityとなるボールのIDを取得
        int BallID = BallIDManager.Instance.GetNewBallID();
        // 受け取った情報を処理する
        //Debug.Log("Received coordinates: " + collisionPoint);
        //Debug.Log("Received tagName: " + TagName);
        // Vector2からVector3に変換
        Vector3 position3D = new Vector3(collisionPoint.x, collisionPoint.y, 0f);

        switch (TagName)
        {
            case "1cherry":
                score += 1;
                GameObject newObj1 = Instantiate(ball2, position3D, Quaternion.identity);
                BallCotroller BallScript = newObj1.GetComponent<BallCotroller>();
                if (BallScript != null)
                {
                    BallScript.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript.ballID = BallID;
                }                
                newObj1.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);

                //newObj1.name = "entityBall"; // オブジェクトの名前を設定
                //演出音とパーティクル
                audio_re.Play();
                GameObject obj1 = Instantiate(prefabCollisionEx,position3D,Quaternion.identity);
                Destroy(obj1, 1.5f);//パーティクル消す
                break;
            case "2strawberry":
                score += 3;
                GameObject newObj2 = Instantiate(ball3, position3D, Quaternion.identity);
                BallCotroller BallScript1 = newObj2.GetComponent<BallCotroller>();
                if (BallScript1 != null)
                {
                    BallScript1.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript1.ballID = BallID;
                }
                newObj2.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_re.Play();
                GameObject obj2 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj2, 1.5f);//パーティクル消す
                break;
            case "3grape":
                score += 6;
                GameObject newObj3 = Instantiate(ball4, position3D, Quaternion.identity);
                BallCotroller BallScript2 = newObj3.GetComponent<BallCotroller>();
                if (BallScript2 != null)
                {
                    BallScript2.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript2.ballID = BallID;
                }
                
                BallScript2.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_mi.Play();
                GameObject obj3 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj3, 1.5f);//パーティクル消す
                break;
            case "4orange":
                score += 10;
                GameObject newObj4 = Instantiate(ball5, position3D, Quaternion.identity);
                BallCotroller BallScript3 = newObj4.GetComponent<BallCotroller>();
                if (BallScript3 != null)
                {
                    BallScript3.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript3.ballID = BallID;
                }
                
                BallScript3.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_fa.Play();
                GameObject obj4 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj4, 1.5f);//パーティクル消す
                break;
            case "5persimmon":
                score += 15;
                GameObject newObj5 = Instantiate(ball6, position3D, Quaternion.identity);
                BallCotroller BallScript4 = newObj5.GetComponent<BallCotroller>();
                if (BallScript4 != null)
                {
                    BallScript4.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript4.ballID = BallID;
                }
                
                BallScript4.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_so.Play();
                GameObject obj5 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj5, 1.5f);//パーティクル消す
                break;
            case "6apple":
                score += 21;
                GameObject newObj6 = Instantiate(ball7, position3D, Quaternion.identity);
                BallCotroller BallScript5 = newObj6.GetComponent<BallCotroller>();
                if (BallScript5 != null)
                {
                    BallScript5.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript5.ballID = BallID;
                }
                BallScript5.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_ra.Play();
                GameObject obj6 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj6, 1.5f);//パーティクル消す
                break;
            case "7pear":
                score += 28;
                GameObject newObj7 = Instantiate(ball8, position3D, Quaternion.identity);
                BallCotroller BallScript6 = newObj7.GetComponent<BallCotroller>();
                if (BallScript6 != null)
                {
                    BallScript6.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript6.ballID = BallID;
                }
                
                BallScript6.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_si.Play();
                GameObject obj7 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj7, 1.5f);//パーティクル消す
                break;
            case "8peach":
                score += 36;
                GameObject newObj8 = Instantiate(ball9, position3D, Quaternion.identity);
                BallCotroller BallScript7 = newObj8.GetComponent<BallCotroller>();
                if (BallScript7 != null)
                {
                    BallScript7.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript7.ballID = BallID;
                }
                
                BallScript7.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj8 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj8, 1.5f);//パーティクル消す
                break;
            case "9pineapple":
                score += 45;
                GameObject newObj9 = Instantiate(ball10, position3D, Quaternion.identity);
                BallCotroller BallScript8 = newObj9.GetComponent<BallCotroller>();
                if (BallScript8 != null)
                {
                    BallScript8.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript8.ballID = BallID;
                }
                
                BallScript8.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj9 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj9, 1.5f);//パーティクル消す
                break;
            case "10melon":
                score += 55;
                GameObject newObj10 = Instantiate(ball11, position3D, Quaternion.identity);
                BallCotroller BallScript9 = newObj10.GetComponent<BallCotroller>();
                if (BallScript9 != null)
                {
                    BallScript9.SetIsHangd(false);//フラグをfalseにするとコンポーネントがつく
                    BallScript9.ballID = BallID;
                }
                
                BallScript9.name = "entityBall_" + BallID.ToString();//名前を変更する
                // BallIDを保存
                ballList.Add(BallID);
                audio_do.Play();
                GameObject obj10 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj10, 1.5f);//パーティクル消す
                break;
            case "11watermelon":
                score += 66;
                //Instantiate(ball6, position3D, Quaternion.identity);
                audio_do.Play();
                GameObject obj11 = Instantiate(prefabCollisionEx, position3D, Quaternion.identity);
                Destroy(obj11, 1.5f);//パーティクル消す
                break;
            default: break;
        }
    }







}
