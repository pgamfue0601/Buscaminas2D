using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public static ButtonsBehaviour Instance;
    public Button salir, reintentar, salirJuego2, salirJuego1, comenzarJuego;
    [SerializeField] Canvas canvas1, canvas2;
    public TextMeshProUGUI felicitacion;
    // Start is called before the first frame update
    void Start()
    {
        canvas2.gameObject.SetActive(false);
        Instance = this;

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
                case 4:
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
        comenzarJuego.onClick.AddListener(() =>
        {
            canvas1.gameObject.SetActive(false);
            canvas2.gameObject.SetActive(true);
        });
        salirJuego1.onClick.AddListener(() => Application.Quit());
        salirJuego2.onClick.AddListener(() => Application.Quit());
        
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
        GameObject.Find("CanvasEnd").SetActive(false);

    }

    public void ButtonsStart()
    {
        salir.gameObject.SetActive(false);
        reintentar.gameObject.SetActive(false);
        salirJuego2.gameObject.SetActive(false);
    }

    
}
