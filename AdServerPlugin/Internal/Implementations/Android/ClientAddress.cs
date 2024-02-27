using UnityEngine;

internal class AndroidClientAddress : IClientAddress
{

    private readonly AndroidJavaObject _clientAddress;

    internal AndroidClientAddress()
    {
        _clientAddress = AndroidAdServer.GetAndroidJavaClass().GetStatic<AndroidJavaObject>("clientProfile").Get<AndroidJavaObject>("clientAddress");
    }
    public override double latitude
    {
        get { return _clientAddress.Get<double>("latitude"); }
        set { _clientAddress.Set("latitude", value); }
    }

    public override double longitude
    {
        get { return _clientAddress.Get<double>("longitude"); }
        set { _clientAddress.Set("longitude", value); }
    }

    public override string country
    {
        get { return _clientAddress.Get<string>("country"); }
        set { _clientAddress.Set("country", value); }
    }

    public override string state
    {
        get { return _clientAddress.Get<string>("state"); }
        set { _clientAddress.Set("state", value); }
    }

    public override string city
    {
        get { return _clientAddress.Get<string>("city"); }
        set { _clientAddress.Set("city", value); }
    }

}