using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Xml.Linq;
using System.Linq.Expressions;

public class Nonogram2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> cellObjects ;

    private bool[,] cellValues;
    private bool[,] solutionArray;
    public SolutionArrayData solutionArrayDataSO;
    public LevelManager levelManager;
    private int mouseColors;
    public int health = 3;
    public PlayerHealth playerHealth;
    public int level;
   
    private void Start()
    {
      //  StartLevel(level);
        mouseColors = 1;

    }
    private void Update()
    {
        bool isMouseDown = false;
        if (Input.GetMouseButtonDown(0))
        {
            // Fare düðmesine basýldýðýnda
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Fare düðmesi serbest býrakýldýðýnda
            isMouseDown = false;
        }

        if (isMouseDown)
        {
            // Týklanan pozisyonu dünya koordinatlarýndan grid içindeki hücre konumuna çevir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int row = Mathf.FloorToInt(mousePosition.x);
            int col = Mathf.FloorToInt(mousePosition.y);
            Debug.Log(row + " mouse " + col);

            UpdateCellVisuals();

        }

    }
    public void ClearPreviousLevel()
    {

     
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Grid");

        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }

        GameObject[] gameObjectsc = GameObject.FindGameObjectsWithTag("GridUI");

        foreach (GameObject gameObjectc in gameObjectsc)
        {
            Destroy(gameObjectc);
        }
        cellObjects.Clear(); // cellObjects listesini temizle
    }
    public void StartLevel(int level) 
    {
    
        this.level = level;
    
        solutionArray = ConvertToBoolArray(solutionArrayDataSO.arrayData[level]);
        cellValues = new bool[solutionArrayDataSO.arrayData[level].rows, solutionArrayDataSO.arrayData[level].columns];
        
    }
    public void AssignCellObjects(GameObject objects)
    {
       
        cellObjects.Add(objects); // Yeni öðeleri ekle
 
    }
    private bool[,] ConvertToBoolArray(BoolGrid arrayData)
    {
        int rows = arrayData.rows;
        int columns = arrayData.columns;
        Debug.Log(rows+"  "+columns);
        bool[,] newArray = new bool[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                bool value = arrayData[row, column];
                newArray[row, column] = value;
            }
        }

        return newArray;
    }

  

    private void UpdateCellVisuals()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int row = Mathf.FloorToInt(mousePosition.y);
        int col = Mathf.FloorToInt(mousePosition.x);
        if (row >= 0 && row < solutionArrayDataSO.arrayData[level].rows && col >= 0 && col < solutionArrayDataSO.arrayData[level].columns)
        {
            bool isSolutionCellTrue = solutionArray[row, col];
            bool isClickedCellTrue = cellValues[row, col];

            GameObject cellObject = cellObjects[row * solutionArrayDataSO.arrayData[level].columns + col];
            SpriteRenderer spriteRenderer = cellObject.GetComponent<SpriteRenderer>();

            if (isSolutionCellTrue && mouseColors == 0 && spriteRenderer != null)
            {
                cellValues[row, col] = true;
                spriteRenderer.color = Color.black;
                IsArraysEqual(solutionArray, cellValues);
                if (IsArraysEqual(solutionArray, cellValues))
                {
                    playerHealth.YouWin(level);

                }
            }
            else if (!isSolutionCellTrue && mouseColors == 1 && spriteRenderer != null)
            {
                cellValues[row, col] = false;
                spriteRenderer.color = Color.white;

                if (IsArraysEqual(solutionArray, cellValues))
                {
                    playerHealth.YouWin(level);

                   
                }
            }
            else
            {
                playerHealth.TakeDamage();
            }

        }
    }
    private bool IsArraysEqual(bool[,] array1, bool[,] array2)
    {
        if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
        {
            // Boyutlar farklý, diziler eþit deðil
            return false;
        }

        for (int i = 0; i < array1.GetLength(0); i++)
        {
            for (int j = 0; j < array1.GetLength(1); j++)
            {
                if (array1[i, j] != array2[i, j])
                {
                    // Deðerler farklý, diziler eþit deðil
                    return false;
                }
            }
        }

        // Tüm deðerler ayný, diziler eþit
        return true;
    }
    public void MouseColor(int a)
    {
        mouseColors = a;
    }
  

}
