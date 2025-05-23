﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace text_together
{
    // 데이터 저장
    class GameSaveState
    {
        public Player player { get; set; }
        public List<Item> inventory { get; set; }
        public List<Item> items { get; set; }
        public Dungeon dungeon { get; set; }

        public List<Quest> quests { get; set; }
        public static String savePath { get; set; }

        public GameSaveState()
        {
        }

        public GameSaveState(Player player, List<Item> inventory, List<Item> items, List<Quest> quests,Dungeon dungeon = null)
        {
            this.player = player;
            this.inventory = inventory;
            this.items = items;
            this.dungeon = dungeon;
            this.quests = quests;

        }

        private const string DefaultSaveFile = "saveFile/slot1.json";

        // 저장 기능
        public static void Save(Player player, List<Item> inventory, List<Item> items, List<Quest> quests, Dungeon dungeon = null,string fileName = null)
        {
            try
            {
                var saveState = new GameSaveState(player, inventory, items, quests, dungeon);
                string json = JsonSerializer.Serialize(saveState, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(savePath ?? DefaultSaveFile, json, Encoding.UTF8);
                Console.WriteLine($"[저장 완료] {fileName ?? DefaultSaveFile}");
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine($"[저장 실패] {ex.Message}");
                }
                catch { }
            }
        }

        // 불러오기 기능
        public static bool TryLoad(out Player player, out List<Item> inventory, out List<Item> items, out List<Quest> quests, out Dungeon dungeon, string fileName = null)
        {
            player = null;
            inventory = null;
            items = null;
            dungeon = null;
            quests = null;

            string path = fileName ?? DefaultSaveFile;


            string folderPath = "saveFile";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            savePath = folderPath + "/" + path;

            if (!File.Exists(folderPath + "/" + path))
                return false;
            try
            {
                string json = File.ReadAllText(savePath, Encoding.UTF8);
                GameSaveState saveState = JsonSerializer.Deserialize<GameSaveState>(json);


                // null 체크 후 초기화
                player = saveState.player;
                inventory = saveState.inventory ?? new List<Item>();
                items = saveState.items ?? new List<Item>();
                dungeon = saveState.dungeon;
                quests = saveState.quests?? new List<Quest>();
                Console.WriteLine("[불러오기 성공]");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[불러오기 실패] {ex.Message}");
                return false;
            }
        }
    }
}