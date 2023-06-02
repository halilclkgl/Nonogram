using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NonogramGrid : MonoBehaviour
{
    [SerializeField] private GameObject[] cellObjects;
    [SerializeField] private TMP_Text[] hintTexts;
    [SerializeField] private TMP_Text[] hintTexts2;
    private bool[,] cellValues;
    private bool[,] solutionArray;
    int gridSize = 4;
    private int mouseColors;
    public int health = 3;
    public PlayerHealth playerHealth;
    private void Start()
    {
        mouseColors = 1;
        solutionArray = GenerateRandomSolution();
        // Hücre deðerleri için 2D array'i oluþturun
        cellValues = new bool[gridSize, gridSize];

        // Hücre objelerini array'e atayýn
        cellObjects = new GameObject[gridSize * gridSize];

        for (int row = 0; row < gridSize; row++)
        {
            List<int> hintValues = new List<int>(); // Ýpucu deðerlerini tutacak liste
            int consecutiveTrueCount = 0; // Ardýþýk true sayýsýný tutacak deðiþken

            for (int col = 0; col < gridSize; col++)
            {
                string cellName = "(" + row + "," + col + ")";
                cellObjects[row * gridSize + col] = GameObject.Find(cellName);

                if (solutionArray[row, col] == true)
                {
                    consecutiveTrueCount++;
                }
                else
                {
                    if (consecutiveTrueCount > 0)
                    {
                        hintValues.Add(consecutiveTrueCount);
                        consecutiveTrueCount = 0;
                    }
                }
            }

            if (consecutiveTrueCount > 0)
            {
                hintValues.Add(consecutiveTrueCount);
            }

            // Ýpucu deðerlerini text kutusuna aktar
            string hintString = string.Join(" ", hintValues.ToArray());
            hintTexts[row].text = hintString;

        }
        for (int row = 0; row < gridSize; row++)
        {
            List<int> hintValues = new List<int>(); // Ýpucu deðerlerini tutacak liste
            int consecutiveTrueCount = 0; // Ardýþýk true sayýsýný tutacak deðiþken

            for (int col = 0; col < gridSize; col++)
            {
                if (solutionArray[col, row] == true)
                {
                    consecutiveTrueCount++;
                }
                else
                {
                    if (consecutiveTrueCount > 0)
                    {
                        hintValues.Add(consecutiveTrueCount);
                        consecutiveTrueCount = 0;
                    }
                }
            }

            if (consecutiveTrueCount > 0)
            {
                hintValues.Add(consecutiveTrueCount);
            }

            string hintString = string.Join(" ", hintValues.ToArray()); // Ýpucu deðerlerini birleþtirerek string haline getirme
            hintTexts2[row].text = hintString;
        }

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Týklanan pozisyonu dünya koordinatlarýndan grid içindeki hücre konumuna çevir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int row = Mathf.FloorToInt(mousePosition.x);
            int col = Mathf.FloorToInt(mousePosition.y);

            // Hücrenin durumunu deðiþtir
            // ToggleCell(row, col);
            UpdateCellVisuals();

        }
    }
    private bool[,] GenerateRandomSolution()
    {
        bool[,] array = new bool[gridSize, gridSize];
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                array[row, col] = Random.value < 0.5f; // %50 olasýlýkla true veya false
            }
        }
        return array;
    }

    private void UpdateCellVisuals()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int row = Mathf.FloorToInt(mousePosition.x);
        int col = Mathf.FloorToInt(mousePosition.y);
        if (row >= 0 && row < gridSize && col >= 0 && col < gridSize)
        {
            bool isSolutionCellTrue = solutionArray[row, col];
            bool isClickedCellTrue = cellValues[row, col];

            GameObject cellObject = cellObjects[row * gridSize + col];
            SpriteRenderer spriteRenderer = cellObject.GetComponent<SpriteRenderer>();

            if (isSolutionCellTrue && mouseColors == 0 && spriteRenderer != null)
            {
                cellValues[row, col] = true;
                spriteRenderer.color = Color.black;
                IsArraysEqual(solutionArray, cellValues);
                if (IsArraysEqual(solutionArray, cellValues))
                {
                    playerHealth.YouWin();
                    Debug.Log("KAZANDIN");
                }
            }
            else if (!isSolutionCellTrue && mouseColors == 1 && spriteRenderer != null)
            {
                cellValues[row, col] = false;
                spriteRenderer.color = Color.white;
               
                if (IsArraysEqual(solutionArray,cellValues))
                {
                    playerHealth.YouWin();
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
