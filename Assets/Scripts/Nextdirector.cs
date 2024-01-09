using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextdirector : MonoBehaviour
{
    // GameDirector インスタンスを格納するための変数
    private GameDirector gameDirector;
    public bool nextball = false; // ネクストボール有無。初期値はfalse


    public GameObject[] balls; // Ball1 から Ball6 までのプレハブを格納する配列
    public Vector3 spawnPoint; // オブジェクトの生成位置。画面で変えられる
    //public string prefabPath; // プレハブのアセットパス（フォルダ内のプレハブへのパス）


    // Start is called before the first frame update
    void Start()
    {
        // GameDirector インスタンスを取得。クリアフラグのために
        gameDirector = FindObjectOfType<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameDirector != null)
        {
            // ゲームディレクターのクリアフラグを確認
            bool isGameClear = gameDirector.IsClear;

            // クリアフラグに応じた処理をここに記述
            if (isGameClear)
            {
                // ゲームがクリアした場合の処理
                //Debug.Log("クリアしてるならなにもしない");
                return;
            }
            else
            {
                // ゲームがクリアしていない場合の処理
                //ボール表示が行われているかをチェック
                //Debug.Log("クリアしていないのでボール有無を");
                if (!nextball)
                {
                    //Debug.Log("ボールないので発生させる");
                    //ボールを出現させる
                    // ランダムなインデックスを生成
                    int randomIndex = Random.Range(0, balls.Length);

                    // ランダムなオブジェクトを生成
                    GameObject randomBall = Instantiate(balls[randomIndex], spawnPoint, Quaternion.identity);
                    // 新しいIDを取得
                    int newBallID = BallIDManager.Instance.GetNewBallID();
                    
                    
                    //IDをボールに登録
                    BallCotroller BallScript = randomBall.GetComponent<BallCotroller>();
                    BallScript.ballID = newBallID;
                    randomBall.name = "NextBall";//ネクストボールの名称。findで探し出せる
                    nextball = true;
                    
            }
            }
        }

    }
}
