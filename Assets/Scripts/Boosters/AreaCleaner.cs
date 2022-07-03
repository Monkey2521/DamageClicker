using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Area cleaner", fileName = "New cleaner booster")]
public sealed class AreaCleaner : Booster
{
    public override void MakeEffect()
    {
        EventBus.Publish<IAreaCleanerHandler>(handler => handler.OnAreaCleaned());
    }
}
