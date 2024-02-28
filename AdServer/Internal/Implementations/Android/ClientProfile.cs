using System;
using UnityEngine;

public class AndroidClientProfile : IClientProfile
{

    AndroidJavaClass _javaGenderEnum = new AndroidJavaClass("com.adgrowth.adserver.entities.ClientProfile$Gender");

    private readonly AndroidJavaObject _clientProfile;

    public AndroidClientProfile()
    {
        _clientProfile = AndroidAdServer.GetAndroidJavaClass().GetStatic<AndroidJavaObject>("clientProfile");
    }
    public override int age
    {
        get { return _clientProfile.Get<int>("age"); }
        set { _clientProfile.Set("age", value); }
    }

    public override int minAge
    {
        get { return _clientProfile.Get<int>("minAge"); }
        set { _clientProfile.Set("minAge", value); }
    }

    public override int maxAge
    {
        get { return _clientProfile.Get<int>("maxAge"); }
        set { _clientProfile.Set("maxAge", value); }
    }

    public override Gender gender
    {
        get { return (Gender)Enum.Parse(typeof(Gender), _clientProfile.Get<string>("gender")); }
        set
        {
            _clientProfile.Set(
                "gender",
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