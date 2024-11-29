using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    public AudioSource SonidoM1911;
    public AudioSource SonidoRecargaM1911;
    public AudioSource SonidoCargadorVacio;

    public AudioSource SonidoAK47;
    

    private void Awake(){

        if(Instance !=null  && Instance!=this  ){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }


    public void SonidoDeDisparo(TiposDeArmas Arma){
        switch(Arma){
            case TiposDeArmas.M1911:
                SonidoM1911.Play();
                break;
            case TiposDeArmas.AK47:
                SonidoAK47.Play();
                break;
            
        }
    }


    public void SonidoRecargarJugar(TiposDeArmas Arma){
        switch(Arma){
            case TiposDeArmas.M1911:
                SonidoRecargaM1911.Play();
                break;
            case TiposDeArmas.AK47:
                SonidoRecargaM1911.Play();
                break;
            
        }
    }
}
