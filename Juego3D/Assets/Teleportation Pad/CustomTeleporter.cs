using UnityEngine;

public class CustomTeleporter : MonoBehaviour
{
    // Configuraciones del teletransporte
    public bool instantTeleport;
    public bool randomTeleport;
    public bool buttonTeleport;
    public string buttonName;
    public bool delayedTeleport;
    public float teleportTime = 3f;
    public string objectTag = ""; // Si está vacío, cualquier objeto será teletransportado
    public Transform[] destinationPad;
    public float teleportationHeightOffset = 1f;

    private float curTeleportTime; // Tiempo restante para teletransporte
    private bool inside; // Verifica si hay algo dentro del trigger
    private bool arrived; // Verifica si el objeto acaba de llegar
    private Transform subject; // Objeto que será teletransportado

    public AudioSource teleportSound;
    public AudioSource teleportPadSound;
    public bool teleportPadOn = true;

    private void Start()
    {
        // Inicializa el temporizador de teletransporte
        curTeleportTime = teleportTime;
    }

    private void Update()
    {
        if (inside && !arrived && teleportPadOn)
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        if (subject == null)
            return;

        Transform rootSubject = subject.root; // Obtener el objeto raíz

        if (instantTeleport)
        {
            PerformTeleport(rootSubject);
        }
        else if (delayedTeleport)
        {
            curTeleportTime -= Time.deltaTime;
            if (curTeleportTime <= 0)
            {
                curTeleportTime = teleportTime; // Reiniciar el temporizador
                PerformTeleport(rootSubject);
            }
        }
        else if (buttonTeleport && Input.GetButtonDown(buttonName))
        {
            PerformTeleport(rootSubject);
        }
    }

    private void PerformTeleport(Transform rootSubject)
    {
        if (randomTeleport)
        {
            int chosenPad = Random.Range(0, destinationPad.Length);
            destinationPad[chosenPad].GetComponent<CustomTeleporter>().arrived = true;
            rootSubject.position = destinationPad[chosenPad].position + Vector3.up * teleportationHeightOffset;
        }
        else
        {
            destinationPad[0].GetComponent<CustomTeleporter>().arrived = true;
            rootSubject.position = destinationPad[0].position + Vector3.up * teleportationHeightOffset;
        }

        // Reproduce el sonido si está configurado
        if (teleportSound != null)
            teleportSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto tiene la etiqueta especificada
        if (string.IsNullOrEmpty(objectTag) || other.CompareTag(objectTag))
        {
            subject = other.transform.root; // Usar el objeto raíz
            inside = true;

            // Si es un teletransporte activado por botón, permite teletransportar nuevamente
            if (buttonTeleport)
            {
                arrived = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale tiene la etiqueta especificada
        if (string.IsNullOrEmpty(objectTag) || other.CompareTag(objectTag))
        {
            if (other.transform.root == subject) // Asegura que es el mismo objeto
            {
                inside = false;
                arrived = false;
                subject = null;
                curTeleportTime = teleportTime; // Reinicia el temporizador
            }
        }
    }
}
