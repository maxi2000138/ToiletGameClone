using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    private PlayerMove[] _playerMoves;
    private EndPoint[] _endPoints;
    private int _drawedLineNum = 0;
    private int _reachedPlayerNum = 0;

    [SerializeField] 
    private GameObject _loosePanel;

    [SerializeField]
    private GameObject _winPanel;
    

    private void Awake()
    {
        ResetDrawedNum();
        ResetReachedPlayerNum();
        _playerMoves = FindObjectsOfType<PlayerMove>();
        _endPoints = FindObjectsOfType<EndPoint>();
    }

    public void ResetDrawedNum() => 
        _drawedLineNum = 0;

    public void ResetReachedPlayerNum() => 
        _reachedPlayerNum = 0;

    private void OnEnable()
    {
        foreach (PlayerMove playerMove in _playerMoves)
        {
            playerMove.OnCanDrawing += OnCanMoving;
            playerMove.OnTriggerOtherPlayer += OnGameLoose;
            playerMove.OnTargetReached += OnTargetReached;

        }

    }

    private void OnDisable()
    {
        foreach (PlayerMove playerMove in _playerMoves)
        {
            playerMove.OnCanDrawing -= OnCanMoving;
            playerMove.OnTriggerOtherPlayer -= OnGameLoose;
            playerMove.OnTargetReached -= OnTargetReached;
        }
        
    }

    private void OnTargetReached()
    {
        _reachedPlayerNum++;
        if (_reachedPlayerNum != _endPoints.Length)
        {
            OnGameWin();
        }
    }

    private void OnCanMoving()
    {
        _drawedLineNum++;
        if (_drawedLineNum == _playerMoves.Length)
        {
            Debug.Log("Start Drawing");
            MoveAll();
        }
        
    }

    private void MoveAll()
    {
        foreach (PlayerMove playerMove in _playerMoves)
        {
            playerMove.Move();
        }
    }

    private void OnGameLoose()
    {
        _loosePanel.SetActive(true);
    }

    public void OnGameReplay()
    {
        foreach (PlayerMove playerMove in _playerMoves) 
            playerMove.ResetPositionAndDeleteLine();
        _loosePanel.SetActive(false);
        _winPanel.SetActive(false);
        ResetDrawedNum();
        ResetReachedPlayerNum();
    }

    public void OnGameWin()
    {
        _winPanel.SetActive(true);
    }
}
