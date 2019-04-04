using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletImpactPrefab;

    void OnEnable()
    {
        GetComponent<Rigidbody>().WakeUp();
        Invoke("HideBullet", 2.0f);
    }

    void OnDisable()
    {
        GetComponent<Rigidbody>().Sleep();
        CancelInvoke();
    }

    private void HideBullet()
    {
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Weapon" && collision.gameObject.tag != "Player")
        {
            BulletHole(collision.gameObject);

            if (collision.gameObject.tag == "Shooting Range Dummies")
            {
                Tutorial tut;
                tut = GameManager.Instance.GetComponent<Tutorial>();

                tut.DummiesShot++;
                Debug.Log("Dummies Shot: " + tut.DummiesShot);
            }
        }
    }

    private void BulletHole(GameObject collidedGameObject)
    {
        for (int i = 0; i <  GameManager.Instance.BulletImpactsPool.Count; i++)
        {
            if (!GameManager.Instance.BulletImpactsPool[i].activeInHierarchy)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 500))
                {
                    if (hit.transform.gameObject == collidedGameObject)
                    {
                        GameManager.Instance.BulletImpactsPool[i].transform.position = hit.point + (hit.normal * 0.025f);
                        GameManager.Instance.BulletImpactsPool[i].transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        GameManager.Instance.BulletImpactsPool[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                        GameManager.Instance.BulletImpactsPool[i].transform.SetParent(collidedGameObject.transform);
                        
                        GameManager.Instance.BulletImpactsPool[i].SetActive(true);
                        gameObject.SetActive(false);
                    }
                }

                break;
            }
        }
    }
}
