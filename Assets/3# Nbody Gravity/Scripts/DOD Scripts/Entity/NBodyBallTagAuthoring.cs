using Unity.Entities;
using UnityEngine;

public class NBodyBallTagAuthoring : MonoBehaviour { }

public class NBodyBallTagBaker : Baker<NBodyBallTagAuthoring>
{
    public override void Bake(NBodyBallTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<NBodyBallTag>(entity);
    }

}
