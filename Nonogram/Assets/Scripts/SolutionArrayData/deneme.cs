using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public SolutionArrayData solutionArrayDataSO;

    private void Start()
    {
        Debug.Log(solutionArrayDataSO.arrayData);
        foreach (var solution in solutionArrayDataSO.arrayData)
        {
            for (int row = 0; row < solution.rows; row++)
            {
                for (int column = 0; column < solution.columns; column++)
                {
                    bool value = solution[row, column];
                 //   Debug.Log($"Row: {row}, Column: {column}, Value: {value}");
                }
            }
        }
    }
}
