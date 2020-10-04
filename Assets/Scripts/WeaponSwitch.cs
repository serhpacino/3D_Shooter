using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public List<Transform> weapons;
    public int initialWeapon;
    public bool autoFill;
    int selectedWeapon;
    void Start()
    {
        selectedWeapon = initialWeapon % weapons.Count;
        UpdateWeapon();
    }
    private void Awake()
    {
        if (autoFill)
        {
            weapons.Clear();
            foreach (Transform weapon in transform)
                weapons.Add(weapon);
        }
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else 
            {
                weapons[i].gameObject.SetActive(false);
            }
        }

    }
   
    void Update()
    {
        //Scroll for changing a weapon
        if (Input.GetAxis("Mouse ScrollWheel")>0)
        {
            selectedWeapon = (selectedWeapon + 1) % weapons.Count;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedWeapon = Mathf.Abs(selectedWeapon - 1) % weapons.Count;
        }
        //buttoms for changing weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&&weapons.Count>1)
        {
            selectedWeapon = 1;
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    selectedWeapon = 2;
        //}
        UpdateWeapon();
    }
}
