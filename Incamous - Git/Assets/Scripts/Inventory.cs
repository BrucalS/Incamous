using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject knife;
    [SerializeField] private Vector3 knifePos;
    [SerializeField] private Quaternion knifeRot;

    private List<GameObject> weaponInv = new List<GameObject>();
    private PickUp pickUp;
    private bool knifeActive = false;

    public List<GameObject> WeaponInv
    {
        get { return weaponInv; }
    }

    // Start is called before the first frame update
    void Start()
    {
        pickUp = GetComponent<PickUp>();
        
        knife.transform.localPosition = knifePos;
        knife.transform.localRotation = knifeRot;
        knife.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (knifeActive)
            knife.SetActive(true);
        else
            knife.SetActive(false);

        if (pickUp.ActiveWeapon != null)
            knifeActive = false;
        else if (pickUp.ActiveWeapon == null)
            knifeActive = true;

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f || Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (WeaponInv.Count > 1)
            {
                if (pickUp.ActiveWeapon == WeaponInv[0])
                {
                    pickUp.ActiveWeapon = WeaponInv[1];
                    WeaponInv[0].SetActive(false);
                } else if (pickUp.ActiveWeapon == WeaponInv[1])
                {
                    pickUp.ActiveWeapon = WeaponInv[0];
                    WeaponInv[1].SetActive(false);
                }
            } else if (WeaponInv.Count == 1)
            {
                pickUp.ActiveWeapon = WeaponInv[0];
            }

            if (pickUp.ActiveWeapon == null && knifeActive && weaponInv.Count >= 1)
            {
                pickUp.ActiveWeapon = WeaponInv[0];
                
                if (WeaponInv.Count > 1)
                    WeaponInv[1].SetActive(false);
            } else if (pickUp.ActiveWeapon == null && !knifeActive)
                return;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!knifeActive && pickUp.ActiveWeapon != null)
            {
                pickUp.ActiveWeapon = null;

                foreach (GameObject weapon in weaponInv)
                {
                    weapon.SetActive(false);
                }

                knifeActive = true;
            }
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        if (WeaponInv.Count < 2)
        {
            weaponInv.Add(weapon);
        } else
        {
            GameObject oldWeapon = pickUp.ActiveWeapon;

            weaponInv.Remove(oldWeapon);

            oldWeapon.transform.parent = null;
            oldWeapon.transform.position = weapon.transform.position;
            oldWeapon.transform.rotation = weapon.transform.rotation;
            oldWeapon.SetActive(true);
            
            weaponInv.Add(weapon);
            pickUp.ActiveWeapon = weapon;
        }

        Debug.Log("Count: " + WeaponInv.Count);
    }
}
