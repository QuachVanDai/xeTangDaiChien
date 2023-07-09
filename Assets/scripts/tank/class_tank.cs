using System;

using UnityEngine;
[Serializable]
public class class_tank
{
   
    public string _name;
    public int _cost;
    public GameObject _prefabs;

    public class_tank(string name, int cost, GameObject prefabs)
    {
     
        _name = name;
        _cost = cost;
        _prefabs = prefabs;
    }

}
