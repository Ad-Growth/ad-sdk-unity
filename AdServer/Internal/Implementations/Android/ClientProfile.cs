using System;
using UnityEngine;

public class AndroidClientProfile : IClientProfile
{
    AndroidJavaClass _javaGenderEnum = new AndroidJavaClass("com.adgrowth.adserver.entities.ClientProfile$Gender");

    private readonly AndroidJavaObject _clientProfile;

    public AndroidClientProfile()
    {
        _clientProfile = AndroidAdServer.GetAndroidJavaClass().CallStatic<AndroidJavaObject>("getClientProfile");
    }
    public override int age
    {
        get { return _clientProfile.Call<int>("getAge"); }
        set { _clientProfile.Call("setAge", value); }
    }

    public override int minAge
    {
        get { return _clientProfile.Call<int>("getMinAge"); }
        set { _clientProfile.Call("setMinAge", value); }
    }

    public override int maxAge
    {
        get { return _clientProfile.Call<int>("getMaxAge"); }
        set { _clientProfile.Call("setMaxAge", value); }
    }

    public override Gender gender
    {
        get { return (Gender)Enum.Parse(typeof(Gender), _clientProfile.Call<string>("getGender")); }
        set
        {
            _clientProfile.Call(
                "setGender",
                _javaGenderEnum.CallStatic<AndroidJavaObject>("valueOf", Enum.GetName(typeof(Gender), value))
            );
        }
    }
    public override IClientAddress clientAddress
    {
        get { return new AndroidClientAddress(); }
    }


    public override void AddInterest(string interest)
    {
        _clientProfile.Call("addInterest", interest);
    }

    public override void RemoveInterest(string interest)
    {
        _clientProfile.Call("removeInterest", interest);
    }
}