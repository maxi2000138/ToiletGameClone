using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Draw : MonoBehaviour
{
    private Vector3 _previousPosition;
    private PlayerMove _player;
    private PlayerInput _playerInput;
    private LineRenderer _lineRenderer;
    private bool _isDrawing = false;

    
    [SerializeField]
    private LineRenderer _lineRendererPrefab;

    [SerializeField] 
    private float _minDistance = 0.1f;

    
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        _playerInput = playerInput;
        
        
       // _playerInput.Player.TouchPress.started += (ctx) => Debug.Log("started press");
        //_playerInput.Player.TouchPress.performed += (ctx) => Debug.Log("performed press");
       // _playerInput.Player.TouchPress.canceled += (ctx) => Debug.Log("canceled press");
        
        //_playerInput.Player.TouchPosition.started += (ctx) => Debug.Log("started position");
        //_playerInput.Player.TouchPosition.performed += (ctx) => Debug.Log("performed position");
       // _playerInput.Player.TouchPosition.canceled += (ctx) => Debug.Log("canceled position");
        

       
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.TouchPress.started += OnTouchDown;
        _playerInput.Player.TouchPress.canceled += OnTouchUp;
        _playerInput.Player.TouchPosition.performed += OnTouchHold;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.TouchPress.started -= OnTouchDown;
        _playerInput.Player.TouchPress.canceled -= OnTouchUp;
        _playerInput.Player.TouchPosition.performed -= OnTouchHold;
    }

    private void OnTouchDown(InputAction.CallbackContext ctx)
    {
        _isDrawing = false;
        var raycast = RaycastTouchPoint();

        if (raycast)
            if (raycast.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
            {
                if(playerMove.ExistLine())
                    return;
                
                _lineRenderer = Instantiate(_lineRendererPrefab);
                playerMove.SetupLineRenderer(_lineRenderer);
                _isDrawing = true;
                _player = playerMove;
            }

        _previousPosition = TouchPosition();
    }

    private void OnTouchHold(InputAction.CallbackContext ctx)
    {
        if (!_isDrawing)
            return;

        Vector3 currentPosition = TouchPosition();
        currentPosition.z = 0f;
        
        if (Vector3.Distance(currentPosition, _previousPosition) >= _minDistance)
        {
            if (_previousPosition == transform.position)
            {
                _lineRenderer.SetPosition(0, currentPosition);
            }
            else
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, currentPosition);
            }

            _previousPosition = currentPosition;
        }
    }

    private void OnTouchUp(InputAction.CallbackContext ctx)
    {
        if (!_isDrawing)
            return;
        
        _isDrawing = false;
        
        RaycastHit2D raycast = RaycastTouchPoint();

        if (raycast)
        {
            if (raycast.collider.gameObject.TryGetComponent(out EndPoint endPoint))
            {
                if (endPoint.IsTarget(_player))
                {
                    Vector3[] positions = new Vector3[_lineRenderer.positionCount];
                    _lineRenderer.GetPositions(positions);
                    _player.OnCanMove(positions);
                    return;
                }
            }
        }

      
        Destroy(_lineRenderer.gameObject);
    }

    private RaycastHit2D RaycastTouchPoint()
    {
        Vector3 currentPosition = TouchPosition();
        RaycastHit2D raycast = Physics2D.Raycast(currentPosition, Vector2.zero);
        return raycast;
    }

    private Vector2 TouchPosition() => 
        Camera.main.ScreenToWorldPoint(_playerInput.Player.TouchPosition.ReadValue<Vector2>());
}
