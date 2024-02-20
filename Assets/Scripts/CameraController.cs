using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject jugador;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        //Diferencia entre la posición de la cámara y la del jugador
        offset = transform.position - jugador.transform.position;

    }

    // Se ejecuta cada frame, pero después de haber procesado todo. Es más exacto para la cámara
    void LateUpdate()
    {
        //Actualizo la posición de la cámara
        transform.position = jugador.transform.position + offset;
    }
}
