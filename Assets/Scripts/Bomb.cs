using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : Item
{
    public override void SetActive(bool isActive)
    {
        base.SetActive(isActive);

        if (isActive == false)
            return;
        
        float lifeTime = Random.Range(MinLifetime, MaxLifetime);
        
        StartCoroutine(nameof(AnnouncingRelease), lifeTime);
        StartCoroutine(nameof(Fading), lifeTime);
    }

    private IEnumerator Fading(float lifeTime)
    {
        float interpolator = 0;
        Color targetColor = DefaultColor;
        targetColor.a = 0;

        while (Renderer.material.color.a > targetColor.a)
        {
            Renderer.material.color = Color.Lerp(DefaultColor, targetColor, interpolator);
            interpolator += DefaultColor.a / lifeTime * Time.deltaTime;
            
            yield return null;
        }
    }
}
