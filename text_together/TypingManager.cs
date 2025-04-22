using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace text_together
{
    class TypingManager
    {

        //음원 경로
        static string filePath = "voice_sans.wav";



        //타이핑 효과
        public void TypingText(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine(text[i]);

                //해당 문자 표현시 소리재생 X
                if (text[i] == ' ' || text[i] == '.')
                {

                }

                else
                {   
                    if (filePath != null)
                    {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                        Thread thread = new Thread(sound);
                        thread.Start();
                    }
                }

                //타이핑 후 대기시간
                Thread.Sleep(75);
            }

        }


        static void sound()
        {
            // AudioFileReader를 사용하여 파일 읽기
            using (var audioFile = new AudioFileReader(filePath))
            {
                // 볼륨 설정: 0.0f(무음) ~ 1.0f(최대)
                audioFile.Volume = 0.5f; //나중에 변수로 빼야할듯

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
