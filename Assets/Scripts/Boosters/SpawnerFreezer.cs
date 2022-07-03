using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Spawner freezer", fileName = "New freezer booster")]
public class SpawnerFreezer : Booster
{
    [SerializeField][Range(1f, 5f)] private float _freezingTime;

    public override void MakeEffect()
    {
        if (_isDebug) Debug.Log("Freeze spawning...");

        EventBus.Publish<ISpawnerFreezeHandler>(handler => handler.OnFreeze());
        WaitEffect();
    }

    async private void WaitEffect()
    {
        await Task.Delay((int)(_freezingTime * 1000));
        
        EventBus.Publish<ISpawnerUnfreezeHandler>(handler => handler.OnUnfreeze());

        if (_isDebug) Debug.Log("Unfreeze spawning");
    }
}
