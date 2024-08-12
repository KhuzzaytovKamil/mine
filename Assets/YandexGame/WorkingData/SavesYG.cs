
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int stone;
        public int coal;
        public int iron;
        public int ironTreasure;
        public int gold;
        public int goldTreasure;
        public int diamond;
        public int emerald;

        public int FoodImprovement;
        public int LuckImprovement;
        public int PickaxeImprovement;

        public int Blocks;

        public SavesYG()
        {
            
        }
    }
}
