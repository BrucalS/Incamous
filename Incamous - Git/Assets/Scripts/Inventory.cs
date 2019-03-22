using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<GameObject> weaponInv = new List<GameObject>();
    private PickUp pickUp;

    public List<GameObject> WeaponInv
    {
        get { return weaponInv; }
    }

    // Start is called before the first frame update
    void Start()
    {
        pickUp = GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f || Input.GetAxis("Mouse ScrollWheel") < 0.0f && WeaponInv.Count > 1)
        {
            if (pickUp.ActiveWeapon == WeaponInv[0])
            {
                pickUp.ActiveWeapon = WeaponInv[1];
                WeaponInv[0].SetActive(false);
            } else
            {
                pickUp.ActiveWeapon = WeaponInv[0];
                WeaponInv[1].SetActive(false);
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
