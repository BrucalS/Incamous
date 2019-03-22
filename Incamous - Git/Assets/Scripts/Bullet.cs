using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletImpactPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Weapon" && collision.gameObject.tag != "Player")
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500))
            {
                if (hit.transform.gameObject == collision.gameObject)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.green);
                    GameObject bulletImpact;
                    bulletImpact = Instantiate(bulletImpactPrefab, collision.gameObject.transform) as GameObject;
                    bulletImpact.transform.position = hit.point + (hit.normal * 0.025f);
                    bulletImpact.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    Debug.Log("Hit the target!");
                    Destroy(gameObject);
                }
            }
        }
    }
}
