using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Target")){
            print("hit: "+ collision.gameObject.name + "!");
            CrearEfectoDeImpacto(collision);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Wall")){
            print("Hit wall");
            CrearEfectoDeImpacto(collision);
            Destroy(gameObject);
        }
    }



    void CrearEfectoDeImpacto(Collision objetoGolpeado){
        ContactPoint contact = objetoGolpeado.contacts[0];

        GameObject agujero = Instantiate(
            GlobalReferences.Instance.EfectoDeBalaPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)

        );

        agujero.transform.SetParent(objetoGolpeado.gameObject.transform);
    }
}