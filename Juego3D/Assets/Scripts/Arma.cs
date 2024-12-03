using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum TiposDeArmas{
    M1911,
    AK47
}

public class Arma : MonoBehaviour
{
    public bool isActiveWeapon;
    public int DanioArma;

    public bool estaDisparando, ListoParaDisparar;
    bool  allowReset  = true;
    public float DelayDisparo = 2f;

    public int BalasPorRafaga = 3;
    public int BalasQueNosQuedan;

    public float Intensidadedispersion;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float velocidadBala=30f;
    public float tiempodevidaBala = 3f;

    public GameObject muzzleEffect;
    internal Animator animator;

    public float tiempoRecarga;
    public int TamanioCargador, balasRestantes;
    public bool estaRecargando;

    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;

    



    public enum ModosDisparo{
        Single,
        Burst,
        Auto
    }

    public TiposDeArmas tipodearma;


    public ModosDisparo DisparoActual;

    private void Awake(){
        ListoParaDisparar = true;
        BalasQueNosQuedan  = BalasPorRafaga;//aqui
        animator = GetComponent<Animator>();
        balasRestantes = TamanioCargador;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {   if(isActiveWeapon){
            if(balasRestantes == 0 && estaDisparando){
                SoundManager.Instance.SonidoCargadorVacio.Play();
            }



            if(DisparoActual ==  ModosDisparo.Auto){
                estaDisparando = Input.GetKey(KeyCode.Mouse0);
            }else if(DisparoActual == ModosDisparo.Single || DisparoActual == ModosDisparo.Burst){
                estaDisparando = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if(Input.GetKeyDown(KeyCode.R)  && balasRestantes <  TamanioCargador && estaRecargando == false){
                Recargar();
            }

            if(ListoParaDisparar && estaDisparando == false && estaRecargando == false && balasRestantes <= 0){
                //Recargar();
            }

            if(ListoParaDisparar && estaDisparando && balasRestantes > 0){
                BalasQueNosQuedan = BalasPorRafaga;//aqui
                DispararArma();

            }

            if(AmmoManager2.Instance.DisplayMunicion != null){
                AmmoManager2.Instance.DisplayMunicion.text = $"{balasRestantes/BalasPorRafaga}/{TamanioCargador/BalasPorRafaga}";
         
            }
        }
    }


    private void DispararArma(){

        balasRestantes--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");
    
        SoundManager.Instance.SonidoDeDisparo(tipodearma);
        ListoParaDisparar = false;
        Vector3 DireccionDisparo = CalcularDireccionYRafaga().normalized;

        GameObject bala = Instantiate(bulletPrefab, bulletSpawn.position , Quaternion.identity);

        Bala bal = bala.GetComponent<Bala>();

        bal.danioBala = DanioArma;


        bala.transform.forward = DireccionDisparo;

        bala.GetComponent<Rigidbody>().AddForce(DireccionDisparo * velocidadBala,ForceMode.Impulse);

        StartCoroutine(DestruirBalaDespuesdeunTiempo(bala, tiempodevidaBala));


        if(allowReset){

            Invoke("ResetShot", DelayDisparo);
            allowReset = false;
        }


        if(DisparoActual  == ModosDisparo.Burst && BalasQueNosQuedan>1){
            BalasQueNosQuedan--; //aqui
            Invoke("DispararArma",DelayDisparo);
        }

    }


    private void Recargar(){

        SoundManager.Instance.SonidoRecargarJugar(tipodearma);

        animator.SetTrigger("RELOAD");
        estaRecargando = true;
        Invoke("RecargaCompletada", tiempoRecarga);
    }

    private void RecargaCompletada(){
        balasRestantes = TamanioCargador;
        estaRecargando = false;
    }


    public void ResetShot(){
        ListoParaDisparar = true;
        allowReset = true;
    }

    public Vector3 CalcularDireccionYRafaga(){
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,  0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }else{
            targetPoint =  ray.GetPoint(100);
        }

        Vector3 direccion = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-Intensidadedispersion, Intensidadedispersion);
        float y = UnityEngine.Random.Range(-Intensidadedispersion, Intensidadedispersion);

        return direccion + new Vector3(x,y,0);
    }


    private IEnumerator DestruirBalaDespuesdeunTiempo(GameObject bala, float tiempodevidaBala){
        yield return new WaitForSeconds(tiempodevidaBala);
        Destroy(bala);
    }
}
