using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum Level
{
    None,
    Low,
    High,
}

public class MicInput : MonoBehaviour
{

    public int micIndex = 0;
    public int sampleLength = 128;
    public float difference = 1f;


    //存放录制的语音
    private AudioClip micRecord;
    private string device;

    private GameManager GM;

    private void Start()
    {
        GM = GameManager.instance;

        //开始麦克风录制
        Debug.Log("Mic Num:" + Microphone.devices.Length);
        device = Microphone.devices[micIndex];
        micRecord = Microphone.Start(device, true, 600, 44100);


    }

    private void Update()
    {
        //取得当前输入的最大音量值
        GM.volume = (float)Math.Round(GetMaxVolume(), 3) * difference;
        //GM.volume = (float)Math.Round(GetAveVolume(), 3) * difference;
        //Debug.Log(volume);


    }

    //获取当前输入的音量最大值
    private float GetMaxVolume()
    {
        float maxVolume = 0f;

        //定义一个float类型的数组用于存储这段录音的音量数组
        float[] volumeData = new float[sampleLength];
        //偏移样本，从当前麦克风所在位置开始读取
        int offset = Microphone.GetPosition(device) - sampleLength + 1;
        if (offset < 0)//麦克风的开始位置通常是负数，规范偏移值
        {
            return 0;
        }

        //从offset位置开始，获取一段录音的数据并存放到volumeData数组中
        micRecord.GetData(volumeData, offset);
        //从取得的数组中找出最大值
        for (int i = 0; i < volumeData.Length; i++)
        {
            float tempMax = volumeData[i];
            if (tempMax > maxVolume)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }

    //获取当前输入的音量平均值
    private float GetAveVolume()
    {
        //定义一个float类型的数组用于存储这段录音的音量数组
        float[] volumeData = new float[sampleLength];
        //偏移样本，从当前麦克风所在位置开始读取
        int offset = Microphone.GetPosition(device) - sampleLength + 1;
        if (offset < 0)//麦克风的开始位置通常是负数，规范偏移值
        {
            return 0;
        }

        //从offset位置开始，获取一段录音的数据并存放到volumeData数组中
        micRecord.GetData(volumeData, offset);
        float sum = 0f;

        //从取得的数组中找出最大值
        for (int i = 0; i < volumeData.Length; i++)
        {
            sum += volumeData[i];
        }
        return sum / sampleLength;
    }

}
