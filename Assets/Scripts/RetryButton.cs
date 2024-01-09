using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ボタンにクリック時の処理を追加
        Button retryButton = GetComponent<Button>();
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Retryボタンがクリックされたときの処理
    void OnRetryButtonClick()
    {
        // ここにRetryボタンがクリックされたときの動作を追加
        Debug.Log("Retryボタンがクリックされました");
        // GameSceneを再読み込みする
        SceneManager.LoadScene("GameScene");
    }
}
