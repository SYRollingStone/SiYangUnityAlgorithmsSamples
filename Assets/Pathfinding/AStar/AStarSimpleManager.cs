using System.Collections;
using System.Collections.Generic;
using Pathfinding.AStar;
using Sirenix.OdinInspector;
using UnityEngine;

public class AStarSimpleManager : MonoBehaviour
{
    // 0是空地，1是起点，2是终点，3是障碍
    public TextAsset matrixMapText;
    public GameObject blockPrefab;
    public float blockPrefabSize = 1;
    
    public static Color normalBlockColor = Color.white;
    public static Color startPointColor = Color.blue;
    public static Color endPointColor = Color.red;
    public static Color obstaclePointColor = Color.magenta;
    public static Color findPointColor = Color.yellow;
    public static Color resultColor = Color.green;
    
    private bool isInitialized;
    private GameObject[,] grid;

    private int[,] matrix;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Dir4AStarAlgorithm dir4AStarAlgorithm = new Dir4AStarAlgorithm(matrix);
                dir4AStarAlgorithm.RunAStar();
            }
        }
    }

    [Button("创建矩阵")]
    public void CreateMatrix()
    {
        if (matrixMapText == null)
        {
            Debug.LogError("matrixMapText is null!");
            return;
        }
        
        string matrixText = matrixMapText.text;
        if (string.IsNullOrEmpty(matrixText))
        {
            Debug.LogError("matrixText string is null!");
            return;
        }
        
        matrix = LoadMatrix(matrixText);
        if (matrix == null)
        {
            Debug.LogError("matrix is null!");
            return;
        }

        DisplayMatrix(matrix);
        isInitialized = true;
    }

    private int[,] LoadMatrix(string matrixText)
    {
        string[] lines = matrixText.Split('\n');
        int rows = lines.Length;
        int cols = lines[0].Split(",").Length;  // 以第一行数据为列长度标准。
        if (rows <= 1)
        {
            Debug.LogError("矩阵请填写大于2行!");
        }
        
        for (int i = 1; i < rows; i++)
        {
            int currentRowCount = lines[i].Split(",").Length;
            if (currentRowCount != cols)
            {
                Debug.LogError($"当前行{i}长度不和第一行长度一致");
                return null;
            }
        }
        
        int[,] matrix = new int[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                matrix[j, i] = int.Parse(values[j].Trim());
            }
        }
        return matrix;
    }

    private void DisplayMatrix(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int columnCount = matrix.GetLength(1);
        
        grid = new GameObject[matrix.GetLength(0), matrix.GetLength(1)];
        // 计算矩阵中心的偏移量
        float offsetX = (columnCount - 1) * blockPrefabSize / 2;
        float offsetY = (rowCount - 1) * blockPrefabSize / 2;
        // 打印矩阵
        string matrixString = "";
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                //int iIndex = rowCount-1 - i;
                int iIndex = i;
                matrixString += matrix[j, iIndex] + " ";
                // 计算每个方块的实例化位置
                float xPos = (j * blockPrefabSize) - offsetX;
                float yPos = (rowCount-1-iIndex * blockPrefabSize) - offsetY;
                
                int matrixValue = matrix[j, iIndex];
                GameObject block = Instantiate(blockPrefab, new Vector3(xPos, yPos), Quaternion.identity);
                block.name = $"Block_{j}_{iIndex}";
                grid[j, iIndex] = block;
                BlockController blockController = block.GetComponent<BlockController>();
                blockController.SetBlockType(matrixValue);
            }
            matrixString += "\n";
        }

        Debug.Log(matrixString);
    }
}
