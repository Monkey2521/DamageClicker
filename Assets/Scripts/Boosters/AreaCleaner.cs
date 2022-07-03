using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Area cleaner", fileName = "New cleaner booster")]
public sealed class AreaCleaner : Booster
{
    public override void MakeEffect()
    {
        if (_isDebug) Debug.Log("Killing all monsters...");

        EventBus.Publish<IAreaCleanerHandler>(handler => handler.OnAreaCleaned());
    }
}
