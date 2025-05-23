using Unity.Entities;
using UnityEngine;

public class SunTagAuthoring : MonoBehaviour { }


/// <summary>
/// A simple tag component useful for identifying entities based on their components
/// </summary>
public class SunTagBaker : Baker<SunTagAuthoring> 
{
    public override void Bake(SunTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<SunTag>(entity);
    }
}

