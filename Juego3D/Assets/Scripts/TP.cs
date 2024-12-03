using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform destination; // Destino del teletransporte
    public float teleportationHeightOffset = 1f; // Offset vertical para el teletransporte

    private void OnTriggerEnter(Collider other)
    {
        // Asegurarnos de que el objeto tiene un Transform válido
        if (other != null && other.transform != null && destination != null)
        {
            // Obtener el objeto raíz del jugador (el padre de Body)
            Transform playerTransform = other.transform.root;

            // Mover el objeto raíz al destino con el offset
            Vector3 newPosition = destination.position + new Vector3(0, teleportationHeightOffset, 0);
            playerTransform.position = newPosition;

            // Imprimir mensaje en consola
            Debug.Log($"{playerTransform.gameObject.name} ha sido teletransportado a {newPosition}");

            // Si el jugador tiene un Rigidbody, ponerlo en modo cinemático durante el teletransporte
            Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;     // Desactivar físicas momentáneamente
            }
        }
        else
        {
            Debug.LogWarning("El destino o el objeto que entra en el trigger no es válido.");
        }
    }
}