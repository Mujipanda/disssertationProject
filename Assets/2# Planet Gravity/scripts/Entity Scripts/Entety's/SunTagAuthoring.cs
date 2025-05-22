using Unity.Entities;
using UnityEngine;

public class SunTagAuthoring : MonoBehaviour { }

public class SunTagBaker : Baker<SunTagAuthoring> 
{
    public override void Bake(SunTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<SunTag>(entity);
    }
}

