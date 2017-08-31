using System.Collections.Generic;
using System;

[Serializable]
public class DataMinion{
    public int ID;
    public string Nombre;
    public string Descripcion;
    public string PathImage;
    public int Ataque;
    public int initialVida;
    public int Vida;
    public int vidaActual;

    public Dictionary<GameTag, bool> Properties = new Dictionary<GameTag, bool>();

    public DataMinion(int iD, string nombre, string descripcion, string pathImage, int ataque, int vida)
    {
        ID = iD;
        Nombre = nombre;
        Descripcion = descripcion;
        PathImage = pathImage;
        Ataque = ataque;
        Vida = vida;
        vidaActual = vida;
        initialVida = vida;
    }  
}
