using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SolutionArrayData", menuName = "CustomData/SolutionArrayData")]
public class SolutionArrayData : ScriptableObject
{

    public BoolGrid[] arrayData;

    // public Image[] arrayToImager;
}
