using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance;

    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    [SerializeField] private int nBombs;
    public Canvas canvas;

    private GameObject[][] map;
    public int retry = 0;

    private int nTest = 0;
    private bool winner = true;
    private int x, y;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;        
    }

    public int getWidth()
    {
        return width;
    }

    public void setWidth(int width) { this.width = width; }

    public int getHeight()
    {
        return height;
    }

    public void setHeight(int height) { this.height = height; }

    public void addTest()
    {
        nTest++;
    }

    public int getNTest() { return nTest; }
    public int getNBombs() { return nBombs; }
    public void setNBombs(int nBombs) { this.nBombs = nBombs; }


    public void EasyMap()
    {
        setWidth(4);
        setHeight(4);
        setNBombs(3);
        retry = 1;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void MediumMap()
    {
        setWidth(5);
        setHeight(5);
        setNBombs(6);
        retry = 2;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void HardMap()
    {
        setWidth(6);
        setHeight(6);
        setNBombs(9);
        retry = 3;

        canvas.gameObject.SetActive(false);
        generateMap();
    }

    public void CustomMap()
    {
        TextMeshProUGUI customWidth = GameObject.Find("WidthText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI customHeight = GameObject.Find("HeightText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI customNBombs = GameObject.Find("BombsText").GetComponent<TextMeshProUGUI>();
        int.TryParse(customWidth.text, out int widthVal);
        int.TryParse(customHeight.text, out int heightVal);
        int.TryParse(customNBombs.text, out int bombsVal);
        setWidth(widthVal);
        setHeight(heightVal);
        setNBombs(bombsVal);
        

        retry = 4;

        
        canvas.gameObject.SetActive(false);
        generateMap();
        
        
    }

    public void generateMap()
    {
        ButtonsBehaviour.Instance.ButtonsStart();
        //Generamos el mapa interno
        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        //Generamos la pantalla de juego
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(celda, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Celda>().setX(i);
                map[i][j].GetComponent<Celda>().setY(j);
            }
        }

        //Situamos la cámara en el centro
        Camera.main.transform.position = new Vector3(((float)width / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);

        //Situamos las bombas aleatoriamente
        for (int i = 0; i < nBombs; i++)
        {
            x = UnityEngine.Random.Range(0, width);
            y = UnityEngine.Random.Range(0, height);
            if (!map[x][y].GetComponent<Celda>().isBomb())
            {
                map[x][y].GetComponent<Celda>().setBomb(true);
            }
            else
            {
                //le restamos menos a la i para que haga una iteración más
                i--;
            }
        }
    }

    public int getBombsAround(int x, int y)
    {
        int contador = 0;

        //la primera comprobacion es para que no se salga de los limites del array y pete
        //casilla superior izquierda
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior, o sea, si existen casillas por encima
        if(y < height-1 && map[x][y+1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior derecha, compruebo que no estoy en el limite derecho
        if(x < width-1 && y < height-1 && map[x + 1][y+1].GetComponent <Celda>().isBomb())
            contador++;

        //casilla izquierda
        if(x > 0 && map[x - 1][y].GetComponent <Celda>().isBomb())
            contador++;

        //casilla derecha
        if(x < width-1 && map[x + 1][y].GetComponent<Celda>().isBomb())
            contador++;

        //Inferior izquieda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior, o sea, si existen casillas por debajo
        if (y > 0 && map[x][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior derecha, compruebo que no estoy en el limite derecho
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //si queremos usar bucles
        //for(int i = x-1; i<=x + 1; i++)
        //{
        //    for(int j = y-1; j >= y + 1; j++)
        //    {
        //        if(i>=0 && i< width && j>= 0 && j< height && i!=x && j != y)
        //        {
        //            if (map[i][j].GetComponent<Celda>().isBomb()) contador++;
        //        }

        //    }
        //}

        return contador;
    }

    public void setWinner(bool win)
    {
        winner = win;
    }

    public bool isWinner()
    {
        return winner;
    }
}
