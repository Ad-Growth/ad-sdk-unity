using System;
using UnityEngine;

public class SDKInitException : Exception
{

    public const string UNAUTHORIZED_CLIENT_KEY = "unauthorized_client_key";
    public const string ALREADY_INITIALIZED = "already_initialized";
    public const string INTERNAL_ERROR = "internal_error";
    public const string NETWORK_ERROR = "network_error";
    public const string UNKNOWN_ERROR = "unknown_error";

    public string code { get; }
    public string message { get; }

    // android overload
    public SDKInitException(AndroidJavaObject reason)
    {
        this.code = reason.Call<string>("getCode");
        this.message = reason.Call<string>("getMessage");
    }
    // TODO: ios overload
    // public SDKInitException(object reason)
    // {
    //     ...
    // }


}
