using UnityEngine;

public class TextBombCreated : TextItemsCreated
{
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _bombSpawner.ItemCreated += ChangeValue;
    }

    private void OnDisable()
    {
        _bombSpawner.ItemCreated -= ChangeValue;
    }
}
