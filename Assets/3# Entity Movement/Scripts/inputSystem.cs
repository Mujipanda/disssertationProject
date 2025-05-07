using Unity.Entities;
using UnityEngine;

public partial class inputSystem : SystemBase
{
    private Controls inputs = null;
    protected override void OnCreate()
    {
        inputs = new Controls();
        inputs.Enable();
    }
    protected override void OnUpdate()
    {
        foreach (RefRW<inputData> data in SystemAPI.Query<RefRW<inputData>>())
        {
            data.ValueRW.move = inputs.Character.Move.ReadValue<Vector2>();

        }
    }
}
