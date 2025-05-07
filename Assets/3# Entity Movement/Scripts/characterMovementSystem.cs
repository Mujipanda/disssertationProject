using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public partial class characterMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        foreach (var (data, inputs, transform) in SystemAPI.Query<RefRO<CharacterData>, RefRO<inputData>, RefRW<LocalTransform>>())
        {
            float3 position = transform.ValueRO.Position;
            position.x += inputs.ValueRO.move.x * data.ValueRO.speed * SystemAPI.Time.DeltaTime;
            position.z += inputs.ValueRO.move.y * data.ValueRO.speed * SystemAPI.Time.DeltaTime;

            transform.ValueRW.Position = position;
        }
    }

}
