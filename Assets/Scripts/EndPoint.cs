using System;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField]
    private List<PlayerMove> _targets;

    public bool IsTarget(PlayerMove playerMove) => 
        _targets.Contains(playerMove);
    
}
