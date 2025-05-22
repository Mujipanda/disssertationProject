using Unity.Entities;
using UnityEngine;

public class BallTagAuthoring : MonoBehaviour {}

public class BallTagBaker : Baker<BallTagAuthoring>
{
    public override void Bake(BallTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<BallTag>(entity);
    }

}



