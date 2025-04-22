using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{

    
    internal class SoundManager
    {
        static string filePath = "../../../../Resources/voice_sans.wav";

        static public void sound()
        {
            // AudioFileReader를 사용하여 파일 읽기
            using (var audioFile = new AudioFileReader(filePath))
            {
                // 볼륨 설정: 0.0f(무음) ~ 1.0f(최대)
                audioFile.Volume = 0.2f; //나중에 변수로 빼야할듯

                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    // 음악이 끝날 때까지 대기
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100); // 100ms마다 상태 확인
                    }
                }
            }
        }



    }
}
