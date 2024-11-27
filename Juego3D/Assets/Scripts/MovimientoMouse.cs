using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMouse : MonoBehaviour
{

    public float sensibilidadMouse = 1000f;

    float rotacionX =  0f;
    float rotacionY = 0f;
    
    public float limitesuperior= -90f;
    public float limiteinferior = 90f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX,limitesuperior,limiteinferior) ;
        rotacionY +=mouseX;
        transform.localRotation = Quaternion.Euler(rotacionX,  rotacionY, 0f);
    }
}
