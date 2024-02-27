using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Object", menuName = "Inventory System/Items/Melee")]
public class MeleeObject : ItemObject
{
    public float range;
    public int damage;
    
    public void Awake()
    {
        type = ItemType.Melee;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
