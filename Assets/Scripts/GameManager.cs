using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Dino          _dino;
    [SerializeField] private TreeGenerator _treeGenerator;
    [SerializeField] private TextMeshProUGUI _tmp;
    private bool                           _gameOn;
    private int                            _points;

    private void Awake()
    {
        _treeGenerator.GameOn = true;
    }
    [Button(enabledMode: EButtonEnableMode.Playmode)]
    private void BoostScore()
    {
        _points += 500;
        Debug.Log("POINTS UP");
    }
    private void Update()
    {
        _gameOn = !_dino.IsDead;
        _points += _dino.Points;
        _treeGenerator.GameOn = _gameOn;
        _treeGenerator.Points = _points;
        _tmp.text = string.Format("Pts : {0:00000}",_points);

        if (!_gameOn && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }
    }
}
