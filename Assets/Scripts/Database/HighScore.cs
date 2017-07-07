namespace Database
{
    [System.Serializable]
    public class HighScore 
    {
        public HighScore(string name,int score)
        {
            this.name = name;
            this.score = score;
        }
        public string name;
        public int score;
    }
}
