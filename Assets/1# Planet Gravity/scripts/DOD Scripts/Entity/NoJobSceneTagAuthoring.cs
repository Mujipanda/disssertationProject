using Unity.Entities;
using UnityEngine;

public class NoJobSceneTagAuthoring : MonoBehaviour { }

/// <summary>
/// A simple tag component useful for identifying entities based on their components
/// </summary>
public class NoJobSceneTagBaker : Baker<NoJobSceneTagAuthoring> 
{
    public override void Bake(NoJobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<NoJobSceneTag>(entity);
    
    }
}
