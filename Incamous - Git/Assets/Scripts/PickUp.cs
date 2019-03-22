using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform bone;
    [SerializeField] private Quaternion weaponRot;
    [SerializeField] private float forcePower;

    private Camera cam;
    private GameObject activeWeapon;
    private GameObject activeThrowable;
    private Inventory inventory;

    public GameObject ActiveWeapon
    {
        get { return activeWeapon; }
        set { activeWeapon = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveWeapon != null)
            ActiveWeapon.SetActive(true);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500))
        {
            Debug.DrawLine (ray.origin, hit.point, Color.red);

            if (hit.collider.tag == "Weapon" && Vector3.Distance(transform.position, hit.transform.position) < range)
            {
                GameObject pickedUpWeapon;
                pickedUpWeapon = hit.transform.gameObject;

                if (Input.GetKeyDown(KeyCode.E) && pickedUpWeapon.transform.parent != transform && ActiveWeapon == null)
                {
                    inventory.AddWeapon(pickedUpWeapon);

                    pickedUpWeapon.transform.parent = bone;
                    pickedUpWeapon.transform.localPosition = pickedUpWeapon.GetComponent<Weapon>().WeaponPos;
                    pickedUpWeapon.transform.localRotation = weaponRot;

                    ActiveWeapon = pickedUpWeapon;
                } else if (Input.GetKeyDown(KeyCode.E) && pickedUpWeapon.transform.parent != transform && ActiveWeapon != null)
                {
                    inventory.AddWeapon(pickedUpWeapon);
                    
                    pickedUpWeapon.transform.parent = bone;
                    pickedUpWeapon.transform.localPosition = pickedUpWeapon.GetComponent<Weapon>().WeaponPos;
                    pickedUpWeapon.transform.localRotation = weaponRot;
                    pickedUpWeapon.SetActive(false);
                }
            } else if (hit.collider.tag == "Throwable" && Vector3.Distance(transform.position, hit.transform.position) < range)
            {
                GameObject pickedUpThrowable;
                pickedUpThrowable = hit.transform.gameObject;

                Rigidbody rb = pickedUpThrowable.GetComponent<Rigidbody>();
                
                if (Input.GetKeyDown(KeyCode.E) && pickedUpThrowable.transform.parent != transform && activeThrowable == null)
                {
                    activeThrowable = pickedUpThrowable;

                    rb.isKinematic = true;
                    pickedUpThrowable.transform.parent = transform;
                    pickedUpThrowable.transform.position = hit.point;
                } else if (Input.GetKeyDown(KeyCode.E) && pickedUpThrowable.transform.parent == transform && activeThrowable != null)
                {
                    rb.isKinematic = false;
                    rb.AddForce(cam.transform.TransformDirection(Vector3.forward) * forcePower);

                    pickedUpThrowable.transform.parent = null;
                    activeThrowable = null;
                }
            }
        }
    }
}
