using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    
    [SerializeField]
    private TextSetuper _textSetuper;

    public override void InstallBindings()
    {
        Container.Bind<TextSetuper>().FromInstance(_textSetuper).AsSingle();
    }
}
