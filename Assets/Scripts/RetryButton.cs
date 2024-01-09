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
        // �{�^���ɃN���b�N���̏�����ǉ�
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

    // Retry�{�^�����N���b�N���ꂽ�Ƃ��̏���
    void OnRetryButtonClick()
    {
        // ������Retry�{�^�����N���b�N���ꂽ�Ƃ��̓����ǉ�
        Debug.Log("Retry�{�^�����N���b�N����܂���");
        // GameScene���ēǂݍ��݂���
        SceneManager.LoadScene("GameScene");
    }
}
