using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Star", menuName = "Star")]
public class Star : ScriptableObject
{
    public string starName;
    public string constellation;
    public float apparentMagnitude;
    public float absoluteMagnitude;
    public int temperature;
    public float mass;
    public float luminosity;
    public float radius;
    public string obervations;

}
