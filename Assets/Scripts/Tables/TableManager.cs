using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;

    [System.Serializable]
    public class Table
    {
        public Transform tableTransform;
        public bool isOccupied = false;
        public GameObject possibleSeats;
    }

    public Table[] tables;
    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    public Transform GetAvailableTable()
    {
        foreach (Table table in tables)
        {
            if (!table.isOccupied)
            {
                table.isOccupied = true;
                return table.tableTransform;
            }

        }
        return null;

    }
    public void FreeTable(Transform tablePosition)
    {
        foreach (Table table in tables)
        {
            if (table.tableTransform == tablePosition)
            {
                table.isOccupied = false;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
