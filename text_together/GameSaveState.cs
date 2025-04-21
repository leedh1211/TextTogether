using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace textRPG
{
    // 데이터 저장
    class GameSaveState
    {
        public Player player { get; set; }
        public List<Item> inventory { get; set; }
        public List<Item> items { get; set; }
        public List<Dungeon> dungeons { get; set; }
        public GameSaveState() { }

        public GameSaveState(Player player, List<Item> inventory, List<Item> items, List<Dungeon> dungeons = null)
        {
            this.player = player;
            this.inventory = inventory;
            this.items = items;
            this.dungeons = dungeons ?? new List<Dungeon>();
        }

        private const string DefaultSaveFile = "player.json";

        // 저장 기능
        public static void Save(Player player, List<Item> inventory, List<Item> items, List<Dungeon> dungeons = null, string fileName = null)
        {
            try
            {
                var saveState = new GameSaveState(player, inventory, items, dungeons);
                string json = JsonSerializer.Serialize(saveState, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName ?? DefaultSaveFile, json, Encoding.UTF8);
                Console.WriteLine($"[저장 완료] {fileName ?? DefaultSaveFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[저장 실패] {ex.Message}");
            }
        }

        // 불러오기 기능
        public static bool TryLoad(out Player player, out List<Item> inventory, out List<Item> items, out List<Dungeon> dungeons, string fileName = null)
        {
            player = null;
            inventory = null;
            items = null;
            dungeons = null;

            string path = fileName ?? DefaultSaveFile;

            if (!File.Exists(path))
                return false;

            try
            {
                string json = File.ReadAllText(path, Encoding.UTF8);
                GameSaveState saveState = JsonSerializer.Deserialize<GameSaveState>(json);


                // null 체크 후 초기화
                player = saveState.player;
                inventory = saveState.inventory ?? new List<Item>();
                items = saveState.items ?? new List<Item>();
                dungeons = saveState.dungeons ?? new List<Dungeon>();

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
