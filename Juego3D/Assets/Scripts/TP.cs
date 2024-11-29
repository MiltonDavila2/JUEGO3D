using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform destino; 
    public float retrasoTP = 0.01f; 
    public bool estaTeletransportando = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !estaTeletransportando)
        {
           
            GameObject jugadorCompleto = other.transform.root.gameObject;

            StartCoroutine(TeletransportarJugador(jugadorCompleto));
        }
    }

    private IEnumerator TeletransportarJugador(GameObject jugador)
    {
        estaTeletransportando = true;

        
        yield return new WaitForSeconds(retrasoTP);

        
        SoundManager.Instance.SonidoTP();
        jugador.transform.position = destino.position;

        
        estaTeletransportando = false;
    }
}
