using Unity.Entities;
using UnityEngine;

public class NoJobSceneTagAuthoring : MonoBehaviour { }

public class NoJobSceneTagBaker : Baker<NoJobSceneTagAuthoring> 
{
    public override void Bake(NoJobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<NoJobSceneTag>(entity);
    
    }
}
