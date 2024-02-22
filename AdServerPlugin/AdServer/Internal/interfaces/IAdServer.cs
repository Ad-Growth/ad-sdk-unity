using System;

public interface IAdServer
{
    public void initialize(Action OnInit, Action<SDKInitException> OnFailed);

}
