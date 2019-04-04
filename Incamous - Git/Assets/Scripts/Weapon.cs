using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Vector3 weaponPos;
    [SerializeField] private Quaternion weaponRot;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int maxMagAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] [Range(0.0f, 2.0f)] private float bulletDelay;

    private float nextBullet = 0.0f;
    private int magAmmo;
    private GameObject bulletPositionHolder;
    private bool isReloading = false;
    private Camera cam;
    private PickUp pickUp;
    private Text reloadingText;

    public Vector3 WeaponPos
    {
        get { return weaponPos; }
    }

    public Quaternion WeaponRot
    {
        get { return weaponRot; }
    }

    public int MagAmmo
    {
        get { return magAmmo; }
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletPositionHolder = transform.GetChild(0).gameObject;
        cam = Camera.main;
        pickUp = GameManager.Instance.Player.GetComponent<PickUp>();
        reloadingText = GameManager.Instance.ReloadingText;

        magAmmo = maxMagAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp.ActiveWeapon == gameObject)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextBullet)
            {
                if (magAmmo > 0 && !isReloading)
                    Shoot();
            } else if (Input.GetKeyDown(KeyCode.R) && magAmmo < maxMagAmmo)
                StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < GameManager.Instance.BulletsPool.Count; i++)
        {
            if (!GameManager.Instance.BulletsPool[i].activeInHierarchy)
            {
                nextBullet = Time.time + bulletDelay;
                
                GameManager.Instance.BulletsPool[i].transform.position = bulletPositionHolder.transform.position;
                Debug.Log("Position: " + GameManager.Instance.BulletsPool[i].transform.position + " " + bulletPositionHolder.transform.position);
                GameManager.Instance.BulletsPool[i].SetActive(true);

                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 500))
                {
                    Rigidbody rb = GameManager.Instance.BulletsPool[i].GetComponent<Rigidbody>();

                    GameManager.Instance.BulletsPool[i].transform.LookAt(hit.point);

                    rb.AddForce((hit.point - transform.position) * bulletSpeed);
                    magAmmo--;
                    Debug.Log(magAmmo);
                }

                break;
            }
        }
    }

    private IEnumerator Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            reloadingText.gameObject.SetActive(true);
            int bulletsToAdd = 0;

            if (maxAmmo >= maxMagAmmo)
                bulletsToAdd = maxMagAmmo - magAmmo;
            else if (maxAmmo < maxMagAmmo)
                bulletsToAdd = maxAmmo;
            else if (maxAmmo == 0)
                yield break;
            
            yield return new WaitForSecondsRealtime(reloadTime);
            reloadingText.gameObject.SetActive(false);

            maxAmmo -= bulletsToAdd;
            magAmmo += bulletsToAdd;

            Debug.Log(magAmmo + ", " + maxMagAmmo + ", " + maxAmmo);

            isReloading = false;
        }
    }
}
