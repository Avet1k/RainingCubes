using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 0.1f;
    
    public event UnityAction<Vector3> Released;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    protected override void Spawn(Cube item)
    {
        item.transform.position = ChooseRandomPosition();
        base.Spawn(item);
    }

    protected override void ReleaseItem(Item item)
    {
        Released?.Invoke(item.transform.position);
        
        base.ReleaseItem(item);
    }
    
    private Vector3 ChooseRandomPosition()
    {
        Vector3 position = transform.position;
        float halfScaleX = transform.localScale.x / 2;
        float halfScaleZ = transform.localScale.z / 2;

        position.x = Random.Range(-halfScaleX, halfScaleX);
        position.z = Random.Range(-halfScaleZ, halfScaleZ);

        return position;
    }
    
    private IEnumerator Spawning()
    {
        bool isWorking = true;
        var delay = new WaitForSeconds(_repeatRate);

        while (isWorking)
        {
            GetItem();
            
            yield return delay;
        }
    }
}
