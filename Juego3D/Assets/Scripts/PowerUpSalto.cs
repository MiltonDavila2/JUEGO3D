using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpSalto : MonoBehaviour
{
    public float duracion = 5f;
    public float aumentoSalto = 2f;
    public string tagJugador = "Player";

    public TextMeshProUGUI textoTiempo; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                StartCoroutine(AplicarPowerUp(jugador));
            }
        }
    }

    private IEnumerator AplicarPowerUp(MovimientoJugador jugador)
    {
        float alturaOriginal = jugador.alturaSalto;
        jugador.alturaSalto += aumentoSalto;

        GetComponent<Collider>().enabled = false;

        
        float tiempoRestante = duracion;

        textoTiempo.color = Color.yellow;

        while (tiempoRestante > 0)
        {
           
            textoTiempo.text = "Salto: " + Mathf.Ceil(tiempoRestante).ToString() + " s";
            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

       
        jugador.alturaSalto = alturaOriginal;
        Destroy(transform.root.gameObject);

        
        textoTiempo.text = "";
    }
}