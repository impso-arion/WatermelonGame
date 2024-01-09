using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControll : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // スライダーコンポーネントをInspectorからアタッチ
    // Start is called before the first frame update
    void Start()
    {
        // スライダーの初期値や最小・最大値の設定
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.value = 0.5f; // 例: 初期値を0.5に設定


    }

    // このメソッドはSliderの値が変更されるたびに呼び出されます
    public void OnVolumeChanged() 
    {
        //Debug.Log("OnVolumeChanged");
        float volume = volumeSlider.value;
        // 音量調節のための処理をここに追加
        //Debug.Log("Volume Changed: " + volume);

        // 例: AudioListener.volumeを変更して実際の音量を変更
        AudioListener.volume = volume;
    }
}
