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
        public int TableID;
        string[] flavors = { "Fresa", "Platano", "Menta" };
        public string assignedFlavor = null;
    }

    public Table[] tables;

    public void AssignFlavorToTable(Transform tableTransform, string flavor)
    {
        // Buscar si alguna mesa con el mismo ID ya tiene un sabor asignado
        Table targetTable = null;
        string existingFlavor = null;

        foreach (Table table in tables)
        {
            if (table.tableTransform == tableTransform)
            {
                targetTable = table;
            }
        }

        if (targetTable != null)
        {
            foreach (Table table in tables)
            {
                if (table.TableID == targetTable.TableID && !string.IsNullOrEmpty(table.assignedFlavor))
                {
                    existingFlavor = table.assignedFlavor;
                    break;
                }
            }

            // Asignar el sabor existente si se encontró, de lo contrario asignar el nuevo sabor
            targetTable.assignedFlavor = existingFlavor ?? flavor;
        }
    }

    public string GetTableFlavorAssigned(Transform tableTransform)
    {
        // Encontrar la mesa objetivo
        Table targetTable = null;
        foreach (Table table in tables)
        {
            if (table.tableTransform == tableTransform)
            {
                targetTable = table;
                break;
            }
        }

        // Si se encuentra la mesa objetivo, buscar sabores asignados en mesas con el mismo ID
        if (targetTable != null)
        {
            foreach (Table table in tables)
            {
                if (table.TableID == targetTable.TableID && !string.IsNullOrEmpty(table.assignedFlavor))
                {

                    return table.assignedFlavor;
                }
            }
        }

        return null;
    }

    private void Awake()
    {
        if (instance == null)
        {
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
                table.isOccupied = true; // Marcar la mesa como en proceso de ser ocupada
                return table.tableTransform;
            }
        }
        return null;
    }

    public void MarkTableAsTaken(Transform tableTransform)
    {
        foreach (Table table in tables)
        {
            if (table.tableTransform == tableTransform)
            {
                table.isOccupied = true;
                return;
            }
        }
    }

    public void FreeTable(Transform tableTransform)
    {
        foreach (Table table in tables)
        {
            if (table.tableTransform == tableTransform)
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
