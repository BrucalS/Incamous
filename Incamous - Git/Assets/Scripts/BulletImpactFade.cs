using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactFade : MonoBehaviour
{
    private float timeToFade = 2.0f;
    private MeshRenderer meshRenderer;
    private Color alphaColor;
    private bool canFade = false;
    
    void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        alphaColor = meshRenderer.material.color;
        alphaColor.a = 0.0f;

        Invoke("HideBulletImpact", 5.0f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFade)
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, alphaColor, timeToFade * Time.deltaTime);
            
            if (meshRenderer.material.color.a <= 0.0001026351f)
                gameObject.SetActive(false);
        }
    }

    private void HideBulletImpact()
    {
        canFade = true;
    }
}
