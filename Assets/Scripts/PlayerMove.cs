using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _startPosition;
    
    [SerializeField] 
    private double _moveTime = 3f;

    private LineRenderer _currentLine = null;
    private Rigidbody2D _rb;
    private float _threshold = 0.1f;
    private double _moveSpeed;

    [SerializeField]
    private Material _drawMaterial;

    private float _timeStep = 0f;
    private Vector3[] _points;
    private Coroutine _moveCoroutine;

    public event Action OnCanDrawing;
    public event Action OnTriggerOtherPlayer;
    public event Action OnTargetReached;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void SetupLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.material = _drawMaterial;
        _currentLine = lineRenderer;
    }

    public bool ExistLine() => 
        _currentLine;

    public void OnCanMove(Vector3[] points)
    {
        _points = points;
        OnCanDrawing?.Invoke();
    }

    public void ResetPositionAndDeleteLine()
    {
        ResetPosition();
        DeleteLine();
    }

    private void ResetPosition() => 
        transform.position = _startPosition;

    private void DeleteLine() => 
        Destroy(_currentLine.gameObject);

    public void Move() => 
        _moveCoroutine = StartCoroutine(MovePlayer(_points));

    public void StopMove() => 
        StopCoroutine(_moveCoroutine);

    IEnumerator MovePlayer(Vector3[] points)
    {
        float length = 0f;
        
        Vector3 startPos = points[0];
        foreach (Vector3 point in points)
        {
            length += (point - startPos).magnitude;
            startPos = point;
        }

        _moveSpeed = length / _moveTime;
        int currentPoint = 0;
        
        while(true)
        {
            if(points.Length == currentPoint)
                break;

            double curSpeed = _moveSpeed * Time.deltaTime;
            while(Vector3.Distance(transform.position, points[currentPoint]) < curSpeed)
            {
                curSpeed -= Vector3.Distance(transform.position, points[currentPoint]);
                transform.position = _points[currentPoint];
                currentPoint++;
                
                if(points.Length == currentPoint)
                    break;
            }
            
            
            if(points.Length == currentPoint)
                break;
            
            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint], (float)curSpeed);
            yield return null;
        }
        
        OnTargetReached?.Invoke();
        Debug.Log("end" + Time.time);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent(out EndPoint endPoint))
        {
            OnTriggerOtherPlayer?.Invoke();
        }
    }
}
