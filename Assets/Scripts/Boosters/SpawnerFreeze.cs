using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Spawner freezer", fileName = "New freezer booster")]
public class SpawnerFreeze : Booster
{
    [SerializeField][Range(1f, 5f)] private float _freezingTime;

    public override void MakeEffect()
    {
        EventBus.Publish<ISpawnerFreezeHandler>(handler => handler.OnFreeze());
        WaitEffect();
    }

    private IEnumerator WaitEffect()
    {
        yield return new WaitForSeconds(_freezingTime);
        Debug.Log("Here");
        EventBus.Publish<ISpawnerUnfreezeHandler>(handler => handler.OnUnfreeze());
    }
}
