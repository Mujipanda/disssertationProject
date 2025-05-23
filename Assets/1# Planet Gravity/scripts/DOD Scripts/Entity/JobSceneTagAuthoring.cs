using Unity.Entities;
using UnityEngine;

public class JobSceneTagAuthoring : MonoBehaviour
{ 

}
/// <summary>
/// A simple tag component useful for identifying entities based on their components
/// </summary>
class Baker : Baker<JobSceneTagAuthoring>
{
    public override void Bake(JobSceneTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent<JobSceneTag>(entity);
    }

}