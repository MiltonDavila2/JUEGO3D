using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance {get; set;}

    public Arma hoveredWeapon = null;

    private void Awake(){

        if(Instance !=null  && Instance!=this  ){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }


    private void Update(){
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            GameObject objectHitByRayCast = hit.transform.gameObject;

            if(objectHitByRayCast.GetComponent<Arma>() && objectHitByRayCast.GetComponent<Arma>().isActiveWeapon  == false){
                hoveredWeapon = objectHitByRayCast.gameObject.GetComponent<Arma>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;
                if(Input.GetKey(KeyCode.F)){
                    WeaponManager.Instance.PickupWeapon(objectHitByRayCast.gameObject);
                }

            }else{
                if(hoveredWeapon){
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }


}
