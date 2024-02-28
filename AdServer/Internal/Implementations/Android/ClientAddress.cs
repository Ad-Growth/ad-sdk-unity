using UnityEngine;

internal class AndroidClientAddress : IClientAddress
{

    private readonly AndroidJavaObject _clientAddress;

    internal AndroidClientAddress()
    {
        _clientAddress = AndroidAdServer.GetAndroidJavaClass().CallStatic<AndroidJavaObject>("getClientProfile").Call<AndroidJavaObject>("getClientAddress");
    }
    public override double latitude
    {
        get { return _clientAddress.Call<double>("getLatitude"); }
        set { _clientAddress.Call("setLatitude", value); }
    }

    public override double longitude
    {
        get { return _clientAddress.Call<double>("getLongitude"); }
        set { _clientAddress.Call("setLongitude", value); }
    }

    public override string country
    {
        get { return _clientAddress.Call<string>("getCountry"); }
        set { _clientAddress.Call("setCountry", value); }
    }

    public override string state
    {
        get { return _clientAddress.Call<string>("getState"); }
        set { _clientAddress.Call("setState", value); }
    }

    public override string city
    {
        get { return _clientAddress.Call<string>("getCity"); }
        set { _clientAddress.Call("setCity", value); }
    }

}