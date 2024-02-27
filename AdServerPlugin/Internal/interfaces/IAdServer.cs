using System;

public abstract class IAdServer
{
    abstract public bool isInitialized { get; }
    abstract public IClientProfile clientProfile { get; }
    public abstract void Initialize(Action OnInit, Action<SDKInitException> OnFailed);

}
