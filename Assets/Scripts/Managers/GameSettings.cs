namespace Managers.GameSettings
{
    /// <summary>
    /// Live Game Settings
    /// </summary>
    public static class GameSettings
    {
        #region Difficulty 
        public enum Difficulty { Easy, Moderate, Insane }

        private static Difficulty GameDifficulty = Difficulty.Easy;

        public static Difficulty GetDifficulty()
        {
            return GameDifficulty;
        }

        public static void SetGameDifficulty(Difficulty newDiff)
        {
            GameDifficulty = newDiff;
        }
        #endregion
    }
}
