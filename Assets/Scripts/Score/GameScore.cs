namespace Assets.Scripts.Score
{
    public class GameScore:IScore
    {
        private int difficulty;
        private int score;

        public void SetDifficulty(int difficulty)
        {
            this.difficulty = difficulty;
        }

        public int IncreaseScoreByVelocity(float velocity)
        {
            int result = 0;
            if (velocity > 5*difficulty + 1)
            {
                if (velocity < 5 * difficulty + 5)
                {
                    result = 5;
                }
                else if (velocity < 5 * difficulty + 10)
                {
                    result = 10;
                }
                else if (velocity < 5 * difficulty + 20)
                {
                    result = 20;
                }
                else
                {
                    result = 30;
                }
                UpdateScore(result);
            }
            return result;
        }

        public int DecreaseScoreByVelocity(float velocity)
        {
            int result = 0;
            if (velocity > 5*difficulty + 1)
            {
                if (velocity < 5 * difficulty + 5)
                {
                    result = -5;
                }
                else if (velocity < 5 * difficulty + 10)
                {
                    result = -10;
                }
                else if (velocity < 5 * difficulty + 20)
                {
                    result = -20;
                }
                else
                {
                    result = -30;
                }
                UpdateScore(result);
            }
            return result;
        }

        private void UpdateScore(int val)
        {
            score += val;
        }

        public int GetScore()
        {
            return score;
        }

        public void Reset()
        {
            score = 0;
        }
    }
}