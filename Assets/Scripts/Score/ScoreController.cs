using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreView view;

    private ScoreModel model;

    private void Start()
    {
        model = new ScoreModel();
        model.ScoreChanged += view.UpdateView;
        
        view.UpdateView(model.Score);
    }

    public void AddScore(int newScore)
    {
        model.AddScore(newScore);
    }
}