public class ScoreModel
{
    public int Score { get; private set; }

    public delegate void OnScoreChanged(int newScore);
    public event OnScoreChanged ScoreChanged;

    public void AddScore(int newScore)
    {
        Score += newScore;
        ScoreChanged?.Invoke(Score);
    }
}
