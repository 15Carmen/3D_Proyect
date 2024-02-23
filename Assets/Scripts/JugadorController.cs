using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class JugadorController : MonoBehaviour
{

    private Rigidbody rb;
    private Vector2 input;
    private Vector3 direction;
    private CharacterController characterController;
    private int contador = 0;

    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    [SerializeField] private float velocidad;
    [SerializeField] private float jumpPower;
    private float caida;

    public TextMeshProUGUI textoContador, textoGanar;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

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
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        characterController.Move(direction * velocidad * Time.deltaTime);
    }

    private void ApplyGravity()
    {

        if (IsGrounded() && caida < 0.0f)
        {
            caida = -1.0f;
        }
        else
        {
            caida = gravity * gravityMultiplier * Time.deltaTime;
        }

        direction.y = caida;
    }

    public void Move(InputAction.CallbackContext context) 
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }
  
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }
        if (!IsGrounded()) { return; }

        caida *= jumpPower;
    }

    private bool IsGrounded() => characterController.isGrounded;

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

