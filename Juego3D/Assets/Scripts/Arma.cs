using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public Camera playerCamera;

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

    public enum ModosDisparo{
        Single,
        Burst,
        Auto
    }


    public ModosDisparo DisparoActual;

    private void Awake(){
        ListoParaDisparar = true;
        BalasQueNosQuedan  = BalasPorRafaga;//aqui
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(DisparoActual ==  ModosDisparo.Auto){
            estaDisparando = Input.GetKey(KeyCode.Mouse0);
        }else if(DisparoActual == ModosDisparo.Single || DisparoActual == ModosDisparo.Burst){
            estaDisparando = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(ListoParaDisparar && estaDisparando){
            BalasQueNosQuedan = BalasPorRafaga;//aqui
            DispararArma();
        }
    }


    private void DispararArma(){

        ListoParaDisparar = false;
        Vector3 DireccionDisparo = CalcularDireccionYRafaga().normalized;

        GameObject bala = Instantiate(bulletPrefab, bulletSpawn.position , Quaternion.identity);

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


    public void ResetShot(){
        ListoParaDisparar = true;
        allowReset = true;
    }

    public Vector3 CalcularDireccionYRafaga(){
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,  0.5f, 0));
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
