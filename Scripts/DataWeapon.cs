using System;
using System.Collections.Generic;

[Serializable]
public class DataWeapon  {

    public int ID;
    public string Nombre;
    public string PathImage;
    public int Ataque;
    public int Durabilidad;
    public Dictionary<GameTag, bool> Properties = new Dictionary<GameTag, bool>();

    public DataWeapon(int iD, string nombre,string pathImage, int ataque, int durabilidad)
    {
        ID = iD;
        Nombre = nombre;
        PathImage = pathImage;
        Ataque = ataque;
        Durabilidad = durabilidad;
    }
}
