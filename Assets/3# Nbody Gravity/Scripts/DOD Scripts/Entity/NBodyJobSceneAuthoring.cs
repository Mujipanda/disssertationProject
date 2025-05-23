using Unity.Entities;
using UnityEngine;

public class NBodyJobSceneTagAuthoring : MonoBehaviour { }

public class NBodyJobSceneTagBaker : Baker<NBodyJobSceneTagAuthoring>
{
    public override void Bake(NBodyJobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<NBodyJobSceneTag>(entity);
    }

}