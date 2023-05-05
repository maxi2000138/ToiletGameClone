using UnityEngine;
using Zenject;

public class LevelRunner : MonoBehaviour
{
    private PlayerMove[] _playerMoves;
    private EndPoint[] _endPoints;
    private SceneLevelChanger _levelChanger;
    private int _drawedLineNum = 0;
    private int _reachedPlayerNum = 0;
    private bool isCompleted = false;
    

    [SerializeField] 
    private GameObject _loosePanel;

    [SerializeField]
    private GameObject _winPanel;

    [SerializeField] 
    private SetLevelText _levelText;

    private GameData _gameData;
    private StaticDataService _staticDataService;


    [Inject]
    public void Construct(SceneLevelChanger levelChanger, GameData gameData, StaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
        _gameData = gameData;
        _levelChanger = levelChanger;
    }
    
    private void Awake()
    {
        ResetDrawedNum();
        ResetReachedPlayerNum();
        SetLevelText();
        _playerMoves = FindObjectsOfType<PlayerMove>();
        _endPoints = FindObjectsOfType<EndPoint>();
        SetSkins();
    }

    public void SetSkins()
    {
        foreach (PlayerMove playerMove in _playerMoves)
        {
            playerPresenter playerPresenter = playerMove.gameObject.GetComponent<playerPresenter>();
            SkinData skinData = _staticDataService.ForSkin(playerPresenter.TypeID
                , _gameData.SkinNum(playerPresenter.TypeID));
            playerPresenter.Construct(skinData.Image);
        }
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
        if (_reachedPlayerNum == _endPoints.Length)
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
        foreach (PlayerMove playerMove in _playerMoves) 
            playerMove.StopMove();
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
        if (!isCompleted)
        {
            _levelChanger.UpgradeLevel();
            isCompleted = true;
        }
    }

    private void SetLevelText() => 
        _levelText.SetText("Level " + _gameData.CurrentLevel);
}
