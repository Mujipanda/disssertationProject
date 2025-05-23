using Unity.Entities;
using UnityEngine;

public class BallTagAuthoring : MonoBehaviour {}

/// <summary>
/// A simple tag component useful for identifying entities based on their components
/// </summary>
public class BallTagBaker : Baker<BallTagAuthoring>
{
    public override void Bake(BallTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<BallTag>(entity);
    }

}



