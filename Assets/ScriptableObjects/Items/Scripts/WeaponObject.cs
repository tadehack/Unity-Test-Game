using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public int maxAmmoInClip;
    public int currentAmmoInClip;
    [Space]
    public int maxReloadAmmo;
    public int currentReloadAmmo;
    [Space]
    public int damage;

    public void Awake()
    {
        type = ItemType.Weapon;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
