using UnityEngine;

public class RewardItem
{
    public int value { get; }
    public string item { get; }

    public RewardItem(AndroidJavaObject rewardItem)
    {
        value = rewardItem.Call<int>("getValue");
        item = rewardItem.Call<string>("getItem");
    }
    
    public override string ToString()
    {
        return "RewardedItem: " + value + " x " + item;
    }

}
