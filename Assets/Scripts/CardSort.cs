//https://www.geeksforgeeks.org/hoares-vs-lomuto-partition-scheme-quicksort/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSort{
    static void Swap<T>(List<T> list, int p1, int p2) {
        T temp = list[p1];
        list[p1] = list[p2];
        list[p2] = temp;
    }

    static int Partition(List<Card> list, int low, int high) {
        Card pivot = list[high];

        // Index of smaller element 
        int i = low - 1;

        for (int j = low; j <= high - 1; j++) {
            // If current element is smaller 
            // than or equal to pivot 
            if (list[j].threat >= pivot.threat) {
                i++; // increment index of 
                     // smaller element 
                Swap(list, i, j);
            }
        }
        Swap(list, i + 1, high);
        return (i + 1);
    }

    public static List<Card> QuickSort(List<Card> list, int low, int high) {
        List<Card> rtn = list;
        if (low < high) {
            /* pi is partitioning index,  
            arr[p] is now at right place */
            int pi = Partition(rtn, low, high);

            // Separately sort elements before 
            // partition and after partition 
            QuickSort(rtn, low, pi - 1);
            QuickSort(rtn, pi + 1, high);
        }
        return rtn;
    }

}
