using UnityEngine;

public class Cube : Item
{
    [SerializeField] private LayerMask _platformLayerMask;
    
    private void OnCollisionEnter(Collision other)
    {
        if (IsCountingLifetime)
            return;
        
        if ((_platformLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            float lifeTime = Random.Range(MinLifetime, MaxLifetime);
            IsCountingLifetime = true;
            Renderer.material.color = ChooseRandomColor();
            StartCoroutine(nameof(AnnouncingRelease), lifeTime);
        }
    }

    private Color ChooseRandomColor()
    {
        float min = 0.001f;
        float max = 1f;

        var color = new Color(
            r: Random.Range(min, max),
            g: Random.Range(min, max),
            b: Random.Range(min, max));

        return color;
    }
}
