using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject CubePrefab;

    class Baker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);
            var spawner = new SpawnerData
            {
                CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic)
            };
            AddComponent(entity, spawner);
        }
    }
}

struct SpawnerData : IComponentData
{
    public Entity CubePrefab;
}