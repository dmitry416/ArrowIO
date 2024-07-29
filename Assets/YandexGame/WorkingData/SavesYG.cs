using System;

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

        //Мои сохранения
        public int coins = 0;
        public int[] openSkins = new int[10] { -1, -1, -1, -1, 0, -1, -1, -1, -1, -1 };
        public int selectedSkin = 4;
        public int selectedStyle = 0;
        public int rating = 0;
        public float musicValue = 1f;
        public float soundValue = 1f;
        public string daylyEnded = "";
        public string nickName = "";


        public SavesYG()
        {
            
        }
    }
}
