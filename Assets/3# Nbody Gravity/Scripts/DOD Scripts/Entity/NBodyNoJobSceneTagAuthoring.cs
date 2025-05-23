using Unity.Entities;
using UnityEngine;

public class NBodyNoJobSceneTagAuthoring : MonoBehaviour { }

public class NBodyNoJobSceneTagBaker : Baker<NBodyNoJobSceneTagAuthoring>
{
    public override void Bake(NBodyNoJobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<NBodyNoJobSceneTag>(entity);
    }

}