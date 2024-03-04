using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public static ButtonsBehaviour Instance;
    public Button salir, reintentar, salirJuego;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        salir.gameObject.SetActive(false);
        reintentar.gameObject.SetActive(false);
        salirJuego.gameObject.SetActive(false);

        reintentar.onClick.AddListener(() =>
        {
            EliminarCasillas();
            switch (Generator.Instance.retry)
            {
                case 1:
                    Generator.Instance.EasyMap();
                    break;
                case 2:
                    Generator.Instance.MediumMap(); 
                    break;
                case 3:
                    Generator.Instance.HardMap(); 
                    break;
                default:
                    Debug.Log("No se ha seleccionado correctamente el mapa");
                    break;
            }
        });

        salir.onClick.AddListener(() =>
        {
            EliminarCasillas();
            Generator.Instance.canvas.gameObject.SetActive(true);
        });
        salirJuego.onClick.AddListener(() => Application.Quit());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EliminarCasillas()
    {
        GameObject[] casillas = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject casilla in casillas) 
        {
            Destroy(casilla);
        }
        Generator.Instance.setWinner(true);

    }

    public void ButtonsStart()
    {
        salir.gameObject.SetActive(false);
        reintentar.gameObject.SetActive(false);
        salirJuego.gameObject.SetActive(false);
    }

    public void ButtonsFinished()
    {
        salir.gameObject.SetActive(true);
        reintentar.gameObject.SetActive(true);
        salirJuego.gameObject.SetActive(true);
    }
}
