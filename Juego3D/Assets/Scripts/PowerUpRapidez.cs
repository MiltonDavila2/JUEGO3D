using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpRapidez : MonoBehaviour
{
    public float duracion = 5f;
    public float incrementoRapidez = 5f;
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
        float rapidezOriginal = jugador.rapidez;
        jugador.rapidez += incrementoRapidez;

        GetComponent<Collider>().enabled = false;

        float tiempoRestante = duracion;

        textoTiempo.color = Color.red; 

        while (tiempoRestante > 0)
        {
            textoTiempo.text = "Rapidez: " + Mathf.Ceil(tiempoRestante).ToString() + " s"; 
            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        jugador.rapidez = rapidezOriginal;
        Destroy(transform.root.gameObject);

        textoTiempo.text = ""; 
    }
}