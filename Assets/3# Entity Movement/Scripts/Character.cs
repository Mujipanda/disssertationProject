using Unity.Entities;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 2f;
}
public struct CharacterData : IComponentData
{
    public float speed;
}// Contains the variables specific for the character

public class CharacterBaker : Baker<Character> // looks for characters with monobeavbiour and bake it with the new icomponentData
{
    public override void Bake(Character authoring)
    {

        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new CharacterData
        {
            speed = authoring.speed
        });
        AddComponent(entity, new inputData
        {
        });
    }
}
