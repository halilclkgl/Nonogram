using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoolGrid
{
    public bool[] data;
    public int rows;
    public int columns;
    public string[] infoRows;
    public string[] infoColumns;
    public int image;
    public bool this[int row, int column]
    {
        get { return data[row * columns + column]; }
        set { data[row * columns + column] = value; }
    }

    public BoolGrid(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        data = new bool[rows * columns];
    }

}
