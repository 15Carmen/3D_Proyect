using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorController : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 movimiento;
    private int contador = 0;

    public float velocidad;
    public TextMeshProUGUI textoContador, textoGanar;



    // Use this for initialization
    void Start()
    {

        //Capturo esa variable al iniciar el juego
        rb = GetComponent<Rigidbody>();

        //Actualizo el texto del contador por pimera vez
        setTextoContador();
        //Inicio el texto de ganar a vacío
        textoGanar.text = "";
    }

    private void Update()
    {

        //Asigno ese movimiento o desplazamiento a mi Rigidbody, multiplicado por la velocidad que quiera darle
        rb.AddForce(movimiento * velocidad);
    }

    // Para que se sincronice con los frames de física del motor
    void FixedUpdate()
    {

        //Estas variables nos capturan el movimiento en horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        //Un vector 3 es un trío de posiciones en el espacio XYZ, en este caso el que corresponde al movimiento
        movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        //Asigno ese movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento);

    }

    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            //Desactivo el objeto
            collision.gameObject.SetActive(false);
            //Incremento el contador en uno (también se peude hacer como contador++)
            contador = contador + 1;
            //Muestro en la consola de depuración el número de coleccionables recogidos
            Debug.Log("Coleccionables recogidos: " + contador);

            //Actualizo el texto del contador
            setTextoContador();
        }

        if (collision.gameObject.CompareTag("TrampaPinchos"))
        {
            //Si se choca con una trampa vuelve al principio del laberinto
            //perdiendo todas las monedas recogidas hasta el momento
            SceneManager.LoadScene(0);
        }
    }

    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador()
    {

        //Para encadenar un texto con una variable, el texto va entre comillas y la variable se encadena con el signo + 
        textoContador.text = "Contador: " + contador.ToString();

        //Para comparar si dos valores son iguales, utilizamos ==
        if (contador == 16)
        {
            textoGanar.text = "¡Ganaste!";
        }

    }
}

