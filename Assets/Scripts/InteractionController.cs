using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
//using UnityEditor.Rendering;

public class InteractionController : MonoBehaviour


{

    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI dialoguefinal;


    private bool isPlayerInRange = false;

    private static int collectedCount = 0;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetCollectedCount()
    {
        collectedCount = 0;
    }


    private void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
            interactAction.action.performed += PerformInteraction;
        }
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.performed -= PerformInteraction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
            Debug.Log("esta en rango");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false);
            Debug.Log("ya no esta en rango");
        }
    }

    private void PerformInteraction (InputAction.CallbackContext context)
    {
        if(isPlayerInRange)
        {
            Debug.Log("Interaccion con objeto");

            interactText.SetActive(false);
            gameObject.SetActive(false);

            if(gameObject.tag == "Cepillo") 
            {
                dialogue.text = "Todos me cuentan sus problemas… ¿quién escuchará los míos? Aún asi me gusta mucho este trabajo";
                collectedCount++;
            }
               
            if (gameObject.tag == "Tijeras")
            {
                dialogue.text = "Cada corte es una oportunidad para hacer a alguien sonreír. ¡Eso me enseñó mi abuela!";
                collectedCount++;
            }
                
            if (gameObject.tag == "Tinte")
            {
                dialogue.text = "¿Un nuevo color, un nuevo comienzo? ¡Me encanta experimentar";
                collectedCount++;
            }
                
            else
            {
                return;
            }
            

            Debug.Log("Objetos recolectados: " + collectedCount);

            // Comprobar si recolecto los 3 objetos
            if (collectedCount >= 3)
            {
                dialoguefinal.text = "¡Has reunido todas las herramientas! Ahora estás lista para comenzar tu trabajo.";
                Debug.Log("¡Recolectaste los 3 objetos!");
                dialogue.gameObject.SetActive(false);
            }

        }
    }
}