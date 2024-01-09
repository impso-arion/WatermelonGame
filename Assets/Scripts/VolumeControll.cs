using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControll : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // �X���C�_�[�R���|�[�l���g��Inspector����A�^�b�`
    // Start is called before the first frame update
    void Start()
    {
        // �X���C�_�[�̏����l��ŏ��E�ő�l�̐ݒ�
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.value = 0.5f; // ��: �����l��0.5�ɐݒ�


    }

    // ���̃��\�b�h��Slider�̒l���ύX����邽�тɌĂяo����܂�
    public void OnVolumeChanged() 
    {
        //Debug.Log("OnVolumeChanged");
        float volume = volumeSlider.value;
        // ���ʒ��߂̂��߂̏����������ɒǉ�
        //Debug.Log("Volume Changed: " + volume);

        // ��: AudioListener.volume��ύX���Ď��ۂ̉��ʂ�ύX
        AudioListener.volume = volume;
    }
}
