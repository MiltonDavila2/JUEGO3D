using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager  Instance { get; private set; }

    public List<GameObject> weaponslots;

    public GameObject ActiveWeaponsSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start(){
        ActiveWeaponsSlot = weaponslots[0];
    }

    private void Update(){
        

        foreach( GameObject weaponslot in weaponslots){
            if(weaponslot == ActiveWeaponsSlot){
                weaponslot.SetActive(true);
            }else{
                weaponslot.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SwitchActiveSlot(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SwitchActiveSlot(1);
        }
    }

    public void PickupWeapon(GameObject pickedupWeapon){
        AddWeapon(pickedupWeapon);
    }

    private void AddWeapon(GameObject pickedupWeapon){

        DropCurrentWeapon(pickedupWeapon);
        pickedupWeapon.transform.SetParent(ActiveWeaponsSlot.transform, false);
        Arma arma = pickedupWeapon.GetComponent<Arma>();

        pickedupWeapon.transform.localPosition = new Vector3(arma.SpawnPosition.x, arma.SpawnPosition.y, arma.SpawnPosition.z );
        pickedupWeapon.transform.localRotation = Quaternion.Euler(arma.SpawnRotation.x, arma.SpawnRotation.y, arma.SpawnRotation.z);

        arma.isActiveWeapon = true;
        
    }

    private void DropCurrentWeapon(GameObject pickedupWeapon){

        if(ActiveWeaponsSlot.transform.childCount>0){
            var weaponToDrop = ActiveWeaponsSlot.transform.GetChild(0).gameObject;
            weaponToDrop.GetComponent<Arma>().isActiveWeapon = false;

           

            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;

        } 
    }

    private void SwitchActiveSlot(int slotnumber){
        if( ActiveWeaponsSlot.transform.childCount > 0){
            Arma currentWeapon = ActiveWeaponsSlot.transform.GetChild(0).GetComponent<Arma>();
            currentWeapon.isActiveWeapon = false;
        }   

        ActiveWeaponsSlot = weaponslots[slotnumber];

        if( ActiveWeaponsSlot.transform.childCount > 0){
            Arma newWeapon = ActiveWeaponsSlot.transform.GetChild(0).GetComponent<Arma>();
            newWeapon.isActiveWeapon = true;
        }   
    }

}
