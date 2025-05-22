using Unity.Entities;
using UnityEngine;

public class JobSceneTagAuthoring : MonoBehaviour
{ 

}

class Baker : Baker<JobSceneTagAuthoring>
{
    public override void Bake(JobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<JobSceneTag>(entity);
    }

}