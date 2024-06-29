using UnityEngine;

public class TextCubesCreated : TextItemsCreated
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.ItemCreated += ChangeValue;
    }

    private void OnDisable()
    {
        _cubeSpawner.ItemCreated -= ChangeValue;
    }
}
