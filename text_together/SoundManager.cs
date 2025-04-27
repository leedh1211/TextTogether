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
        static string filePath = "../../../../Resources/UnderTail/VoiceSans.wav";//
        static string sans_Sound = "../../../../Resources/UnderTail/VoiceSans.wav";//


        static string Pokemon_BW_Elite_Four_Battle = "../../../../Resources/Pokemon/Pokemon_BW_Elite_Four_Battle.mp3";//0
        static string Pokemon_BW_Reshiram_Zekrom = "../../../../Resources/Pokemon/Pokemon_BW_Reshiram_Zekrom.mp3";//1
        static string Pokemon_BW_Rival_Battle = "../../../../Resources/Pokemon/Pokemon_BW_Rival_Battle.mp3";//2
        static string Pokemon_BW_Trainer_Battle = "../../../../Resources/Pokemon/Pokemon_BW_Trainer_Battle.mp3";//3
        static string Undertale_Megalovania = "../../../../Resources/UnderTail/Undertale_Megalovania.mp3";//4
        static string Undertale_Spear_of_Justice = "../../../../Resources/UnderTail/Undertale_Spear_of_Justice.mp3";//5
        static string Blue_Archive_OperationD = "../../../../Resources/Other/Blue_Archive_OperationD.mp3";//6
        static string ChronoArk_Sir_Dorchi = "../../../../Resources/Other/ChronoArk_Sir_Dorchi.mp3";//7
        static string HealingSong_Remix = "../../../../Resources/Other/HealingSong_Remix.mp3";//8


        static string UnderTail_Shop = "../../../../Resources/UnderTail/UnderTail_Shop.mp3";//9
        static string Undertail_Lancer = "../../../../Resources/UnderTail/Undertail_Lancer.mp3"; //10
        static string Maple_Sleepywood = "../../../../Resources/Other/Maple_Sleepywood.mp3";  //11
        static string Maple_elnas = "../../../../Resources/Other/Maple_elnas.mp3";  //12
        static string Maple_Temple_of_time = "../../../../Resources/Other/Maple_Temple_of_time.mp3"; //13
        static string Maple_orbis = "../../../../Resources/Other/Maple_orbis.mp3"; //14
        static string Maple_henesis = "../../../../Resources/Other/Maple_henesis.mp3"; //15


        static string Pokemon_A_Button_Sound_Effect = "../../../../Resources/Effect/Pokemon_A_Button_Sound_Effect.mp3";

        static bool isSoundPlay = false;

        static public List<string> BGM_List = new List<string>();
        static public List<string> Effect_List = new List<string>();

        static public void Sound_Init()
        {
            BGM_List.Add(Pokemon_BW_Elite_Four_Battle); //0
            BGM_List.Add(Pokemon_BW_Reshiram_Zekrom);   //1
            BGM_List.Add(Pokemon_BW_Rival_Battle);      //2
            BGM_List.Add(Pokemon_BW_Trainer_Battle);    //3
            BGM_List.Add(Undertale_Megalovania);        //4
            BGM_List.Add(Undertale_Spear_of_Justice);   //5
            BGM_List.Add(Blue_Archive_OperationD);      //6
            BGM_List.Add(ChronoArk_Sir_Dorchi);         //7
            BGM_List.Add(HealingSong_Remix);            //8
            BGM_List.Add(UnderTail_Shop);               //9
            BGM_List.Add(Undertail_Lancer);             //10
            BGM_List.Add(Maple_Sleepywood);             //11
            BGM_List.Add(Maple_elnas);                  //12
            BGM_List.Add(Maple_Temple_of_time);         //13
            BGM_List.Add(Maple_orbis);                  //14
            BGM_List.Add(Maple_henesis);                //15



            Effect_List.Add(Pokemon_A_Button_Sound_Effect);

        }

        


        
        static public void sound()
        {
            filePath = sans_Sound;

            // AudioFileReader를 사용하여 파일 읽기
            using (var audioFile = new AudioFileReader(filePath))
            {
                // 볼륨 설정: 0.0f(무음) ~ 1.0f(최대)
                audioFile.Volume = 0.15f; //나중에 변수로 빼야할듯

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

        static public void EffectPlay()
        {
            filePath = Effect_List[0];

            // AudioFileReader를 사용하여 파일 읽기
            using (var audioFile = new AudioFileReader(filePath))
            {
                // 볼륨 설정: 0.0f(무음) ~ 1.0f(최대)
                audioFile.Volume = 0.15f; //나중에 변수로 빼야할듯

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



        static object lockObj = new object(); // 락용 객체
        static public void BGM(int index)
        {
            

            lock (lockObj)
            {

                if (isSoundPlay == false)
                {
                    isSoundPlay = true;
                }



                // AudioFileReader를 사용하여 파일 읽기
                using (var audioFile = new AudioFileReader(BGM_List[index]))
                {
                    // 볼륨 설정: 0.0f(무음) ~ 1.0f(최대)
                    audioFile.Volume = 0.05f; //나중에 변수로 빼야할듯

                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();

                        // 음악이 끝날 때까지 대기
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            // 여기서 조건 체크
                            if (isSoundPlay == false)
                            {
                                outputDevice.Stop(); // 음악 강제 종료
                                break;
                            }

                            Thread.Sleep(100); // 100ms마다 상태 확인
                        }
                    }
                }
            }
        }

        static public void Stop_BGM()
        {
            isSoundPlay = false;
        }

    }
}
