using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private CharacterController  controller;
    public float rapidez = 12f;
    public float gravedad = -9.81f  *2;

    public float alturaSalto = 3f;

    public Transform CheckearPiso;

    public float distanciaSuelo = 2f;

    public LayerMask MascaraSuelo;

    Vector3 velocidad;
    
    bool EstaPiso;

    bool EstaMoviendose;

    private Vector3 UltimaPosicion = new  Vector3(0f,0f,0f);


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    EstaPiso = Physics.CheckSphere(CheckearPiso.position, distanciaSuelo, MascaraSuelo);


    if(EstaPiso && velocidad.y < 0){
        velocidad.y = -2f; 
    }

    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    
    Vector3 mover = transform.right * x + transform.forward * z;

    controller.Move(mover * rapidez * Time.deltaTime);

    
    if(Input.GetButtonDown("Jump") && EstaPiso){
        velocidad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad); 
    }

    
    velocidad.y += gravedad * Time.deltaTime;

   
    controller.Move(velocidad * Time.deltaTime);

    
    EstaMoviendose = UltimaPosicion != gameObject.transform.position && EstaPiso;
    UltimaPosicion = gameObject.transform.position;
    }

}
