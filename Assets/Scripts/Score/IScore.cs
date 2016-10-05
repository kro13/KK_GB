namespace Assets.Scripts.Score
{
    public interface IScore
    {
        void SetDifficulty(int difficulty);
        int IncreaseScoreByVelocity(float velocity);
        int DecreaseScoreByVelocity(float velocity);
        int GetScore();
        void Reset();
    }
}