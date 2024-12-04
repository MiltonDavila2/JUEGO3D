using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpGravedad : MonoBehaviour
{
    public float duracion = 5f;
    public float cambioGravedad = -4.9f;
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
        float gravedadOriginal = jugador.gravedad;
        jugador.gravedad += cambioGravedad;

        GetComponent<Collider>().enabled = false;

        float tiempoRestante = duracion;

        textoTiempo.color = Color.blue; 

        while (tiempoRestante > 0)
        {
            textoTiempo.text = "Gravedad: " + Mathf.Ceil(tiempoRestante).ToString() + " s"; 
            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        jugador.gravedad = gravedadOriginal;
        Destroy(transform.root.gameObject);

        textoTiempo.text = ""; 
    }
}
