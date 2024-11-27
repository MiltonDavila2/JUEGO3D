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

    // Asegúrate de que la velocidad vertical sea correcta cuando el jugador toca el suelo
    if(EstaPiso && velocidad.y < 0){
        velocidad.y = -2f; // Puedes ajustar este valor si es necesario
    }

    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    // Movimiento en X y Z (horizontal)
    Vector3 mover = transform.right * x + transform.forward * z;

    controller.Move(mover * rapidez * Time.deltaTime);

    // Salto: asegura que la gravedad y el cálculo del salto sean correctos
    if(Input.GetButtonDown("Jump") && EstaPiso){
        velocidad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad); 
    }

    // Aplicación de gravedad
    velocidad.y += gravedad * Time.deltaTime;

    // Movimiento vertical (gravedad)
    controller.Move(velocidad * Time.deltaTime);

    // Verificación de movimiento
    EstaMoviendose = UltimaPosicion != gameObject.transform.position && EstaPiso;
    UltimaPosicion = gameObject.transform.position;
    }

}
