using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SnapScrolling : MonoBehaviour
{
    private int _goodAmount;

    [SerializeField, Range(0,500)] 
    private float _goodOffset;

    [SerializeField, Range(0,5)] 
    private float _scaleOffset;

    [SerializeField, Range(1,60)] 
    private float _scaleSpeed;

    [SerializeField, Range(0f, 20f)] 
    private float _snapSpeed;
    
    [SerializeField, Range(200f,1000f)] 
    private float _inertiaStoppingSpeed;
    
    [SerializeField] 
    private GoodPresenter _goodPrefab;

    [SerializeField]
    private ScrollRect _scrollRect;

    [SerializeField] 
    private SkinTypeID _skinTypeID;

    private RectTransform _contentRect;

    private Vector2 _contentVector;

    private GoodPresenter[] _goods;

    private Vector2[] _goodsPos;

    private Vector2[] _goodsScale;

    private float _sizeX;

    private int _selectedGoodID = 0;

    private bool _isScrolling;
    
    private StaticDataService _staticDataService;

    public event Action<int> OnSelectedGoodChanged;

    [Inject]
    public void Construct(StaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    private void Start()
    {
        _goodAmount = _staticDataService.SkinAmount(_skinTypeID);
        _contentRect = GetComponent<RectTransform>();
        _sizeX = Mathf.Abs(_goodPrefab.GetComponent<RectTransform>().sizeDelta.x);
        _goods = new GoodPresenter[_goodAmount];
        _goodsPos = new Vector2[_goodAmount];
        _goodsScale = new Vector2[_goodAmount];
        for (int i = 0; i < _goodAmount; i++)
        {
            _goods[i] = Instantiate(_goodPrefab, transform, false);
            SkinData data = _staticDataService.ForSkin(_skinTypeID, i);
            _goods[i].Construct(data.Image, data.Name);
            if (i == 0)
            {
                _goodsPos[0] = _goods[0].transform.localPosition;
                continue;
            }

            _goods[i].transform.localPosition = 
                new Vector2(
                    _goods[i-1].transform.localPosition.x + _sizeX + _goodOffset
                    ,_goods[i].transform.localPosition.y);
            _goodsPos[i] = -_goods[i].transform.localPosition;

        }
    }
        
    
    private void FixedUpdate()
    {
        if ((_contentRect.anchoredPosition.x >= _goodsPos[0].x && !_isScrolling) ||
            (_contentRect.anchoredPosition.x <= _goodsPos[_goodAmount - 1].x && !_isScrolling))
        {
            _isScrolling = false;
            _scrollRect.inertia = false;
        }

        int selectedID = -1;
        float nearestPos = float.MaxValue;
        
        if (_isScrolling) return;
        for (int i = 0; i < _goodAmount; i++)
        {
            float distance = Mathf.Abs(_contentRect.anchoredPosition.x - _goodsPos[i].x);
            
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedID = i;
            }

            if (selectedID != _selectedGoodID)
            {
                _selectedGoodID = selectedID;
                OnSelectedGoodChanged?.Invoke(_selectedGoodID);
            }

            float scale = Mathf.Clamp(1 / (distance / _goodOffset) * _scaleOffset, 0.5f, 1f);
            _goodsScale[i].x = Mathf.SmoothStep(_goods[i].transform.localScale.x, scale, _scaleSpeed * Time.fixedDeltaTime);
            _goodsScale[i].y = Mathf.SmoothStep(_goods[i].transform.localScale.y, scale, _scaleSpeed * Time.fixedDeltaTime);
            _goods[i].transform.localScale = _goodsScale[i];
        }
        
        float scrollVelocity = Mathf.Abs(_scrollRect.velocity.x);
        if (scrollVelocity < _inertiaStoppingSpeed && !_isScrolling) _scrollRect.inertia = false;
        if(scrollVelocity >= _inertiaStoppingSpeed || _isScrolling) return;
        
        _contentVector.x = Mathf.SmoothStep(_contentRect.anchoredPosition.x, _goodsPos[_selectedGoodID].x, _snapSpeed * Time.fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }

    public void SetScrolling(bool scrolling)
    {
        _isScrolling = scrolling;
        if (_isScrolling)
            _scrollRect.inertia = true;
    }
}
