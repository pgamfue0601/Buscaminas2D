using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Celda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private Canvas bombImage;

    private void Start()
    {
        bombImage.gameObject.SetActive(false);
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return this.y;
    }

    public int getX()
    {
        return this.x;
    }

    public void setBomb(bool bomb)
    {
        this.bomb = bomb;
    }
    
    public bool isBomb()
    {
        return bomb;
    }

    public void setText(string text)
    {
        this.tmpText.text = text;
    }

    private void OnMouseDown()
    {
        if (Generator.Instance.isWinner())
        {

            if (this.bomb)
            {
                GetComponent<SpriteRenderer>().material.color = Color.red;
                bombImage.gameObject.SetActive(true);
                Debug.Log("Has perdido");
                Generator.Instance.setWinner(false);
                ButtonsBehaviour.Instance.felicitacion.gameObject.SetActive(false);
                GameObject.Find("CanvasEnd").SetActive(true);
            }
            else
            {
                tmpText.text = Generator.Instance.getBombsAround(x, y).ToString();
                Generator.Instance.addTest();
                if ((Generator.Instance.getWidth() * Generator.Instance.getHeight()) - Generator.Instance.getNBombs() == Generator.Instance.getNTest())
                {
                    Debug.Log("Has ganado el juego");
                    ButtonsBehaviour.Instance.felicitacion.gameObject.SetActive(true);
                    GameObject.Find("CanvasEnd").SetActive(true);
                }
            }
        }
        else return;
        
    }

}
