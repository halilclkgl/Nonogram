using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject gridPrefab;
    public Nonogram2 nonogram2;
    public GameObject hintTexts;

    int level;
 
    public void StartLeveD(int level) 
    {
        this.level = level;
        SolutionArrayData solutionArrayDataSO = nonogram2.solutionArrayDataSO;
        int gridSize = solutionArrayDataSO.arrayData[level].rows;
        int gridSize2 = solutionArrayDataSO.arrayData[level].columns;
        SpawnGrids(gridSize, gridSize2);

    }

    private void SpawnGrids(int gridSize, int gridSize2)
    {
        float gridSizeFloat = gridSize; // Grid boyutunu float olarak saklayýn

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize2; col++)
            {
                // Tam sayý koordinatlarý için 0.5 ekleyin
                float x = col + 0.5f;
                float y = row + 0.5f;

                GameObject grid = Instantiate(gridPrefab, new Vector3(x, y, 0), Quaternion.identity);
                grid.name = "(" + row + "," + col + ")";
                grid.transform.SetParent(transform);
                nonogram2.AssignCellObjects(grid);
                if (col==0)
                {
                    SolutionArrayData solutionArrayDataSO = nonogram2.solutionArrayDataSO;
                    GameObject hintTexts2 = Instantiate(hintTexts, new Vector3(x-1, y, 0), Quaternion.identity);
                    hintTexts2.transform.SetParent(transform);
                    TextMeshProUGUI textMesh = hintTexts2.GetComponentInChildren<TextMeshProUGUI>();
                    textMesh.text = solutionArrayDataSO.arrayData[level].infoRows[row];
                }
                if (row==0)
                {
                    SolutionArrayData solutionArrayDataSO = nonogram2.solutionArrayDataSO;
                    GameObject hintTexts2 = Instantiate(hintTexts, new Vector3(x , gridSize+0.5f, 0), Quaternion.identity);
                    hintTexts2.transform.SetParent(transform);
                    TextMeshProUGUI textMesh = hintTexts2.GetComponentInChildren<TextMeshProUGUI>();
                    textMesh.text = solutionArrayDataSO.arrayData[level].infoColumns[col];
                }  
            }
        }
       
    }

}
