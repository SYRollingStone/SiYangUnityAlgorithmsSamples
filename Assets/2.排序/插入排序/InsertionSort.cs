using System;
using UnityEngine;

namespace _2.排序.插入排序
{
    public class InsertionSort : MonoBehaviour
    {
        public int[] data = { 4,8,1,6,0,5,3,9,7,2,11,12 };

        private void Start()
        {
            int [] result = Sort();
            DebugLog(result);
        }

        private int [] Sort()
        {
            int [] result = new int[data.Length];
            result[0] = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                int key = data[i];
                int j = i - 1;
                while (j >= 0 && result[j] > key)
                {
                    result[j + 1] = result[j];
                    j--;
                }
                result[j + 1] = key;
            }
            return result;
        }

        private void DebugLog(int [] result)
        {
            string debugStr = "";
            foreach (var p in result)
            {
                debugStr += p + ",";
            }
            Debug.Log(debugStr);
        }
    }
}