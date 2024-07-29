
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        public int coal;
        public int copper;
        public int iron;
        public int gold;
        public int diamond;
        public int emerald;
        public int score;
        public int goalNumber;
        public int EnergyImprovement;
        public int LuckImprovement;
        public int Energy;
        public int Luck;
        public int Blocks;
        public int numberOfPickaxe;
        public int numberOfPickaxe1;
        public int numberOfPickaxe2;
        public int numberOfPickaxe3;
        public int numberOfPickaxe4;
        public int numberOfPickaxe5;
        public int IsGameCompleted;

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
