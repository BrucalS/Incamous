using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactFade : MonoBehaviour
{
    private float timeToFade = 2.0f;
    private MeshRenderer meshRenderer;
    private Color alphaColor;
    private bool canFade = false;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        alphaColor = meshRenderer.material.color;
        alphaColor.a = 0.0f;

        StartCoroutine(TimeBeforeFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (canFade)
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, alphaColor, timeToFade * Time.deltaTime);
            
            if (meshRenderer.material.color.a <= 0.0001026351f)
                Destroy(gameObject);
        }
    }

    private IEnumerator TimeBeforeFade()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        canFade = true;
    }
}
