using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _nextPositioin;

    private void OnEnable()
    {
        _cubeSpawner.Released += SetPosition;
    }

    private void OnDisable()
    {
        _cubeSpawner.Released -= SetPosition;
    }

    protected override void Spawn(Bomb item)
    {
        item.transform.position = _nextPositioin;
        base.Spawn(item);
    }

    private void SetPosition(Vector3 position)
    {
        _nextPositioin = position;
        GetItem();
    }
}

