using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public enum CustomerState { Entering, Seated, Waiting, Served }
    public CustomerState state = CustomerState.Entering;
    [SerializeField]
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    public string requestedFlavor;
    private Transform assignedTable;
    public GameObject panel;
    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent.speed = 3f;
        obstacle.enabled = false; // Deshabilitar el obstáculo al inicio
        Transform availableTableTransform = TableManager.instance.GetAvailableTable();
        collider.enabled = false;
        if (availableTableTransform != null)
        {
            AssignTable(availableTableTransform);
        }
        else
        {
            Debug.LogWarning("No hay mesas disponibles para este cliente.");
        }
    }

    private void Update()
    {
        if (state == CustomerState.Entering && assignedTable != null)
        {
            if (Vector3.Distance(transform.position, assignedTable.position) < 2f)
            {
                TakeSeat();
                collider.enabled = true;
                RequestFlavor();
            }
            else
            {
                agent.SetDestination(assignedTable.position);
            }
        }
    }

    private void TakeSeat()
    {
        state = CustomerState.Seated;
        TableManager.instance.MarkTableAsTaken(assignedTable); // Marcar la mesa como ocupada al sentarse
        Sit(assignedTable);
    }

    private void Sit(Transform table)
    {
        agent.SetDestination(table.position);
        if (agent.remainingDistance < 1f)
        {
            agent.enabled = false;
            obstacle.enabled = true;
            gameObject.transform.position = table.position;
            gameObject.transform.rotation = table.rotation;
        }
    }

    private void AssignTable(Transform tablePosition)
    {
        assignedTable = tablePosition;
        agent.SetDestination(assignedTable.position);
    }

    private void RequestFlavor()
    {
        string tableFlavor = TableManager.instance.GetTableFlavorAssigned(assignedTable);
        if (string.IsNullOrEmpty(tableFlavor))
        {
            requestedFlavor = GetRandomFlavor();
            TableManager.instance.AssignFlavorToTable(assignedTable, requestedFlavor);
        }
        else
        {
            requestedFlavor = tableFlavor;
        }
        
        panel.SetActive(true);

        // Desactivar todos los sabores
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Activar el sabor solicitado
        switch (requestedFlavor)
        {
            case "Fresa":
                panel.transform.Find("Fresa_Sabor").gameObject.SetActive(true);
                break;
            case "Platano":
                panel.transform.Find("Platano_Sabor").gameObject.SetActive(true);
                break;
            case "Menta":
                panel.transform.Find("Menta_Sabor").gameObject.SetActive(true);
                break;
        }

        state = CustomerState.Waiting;
    }

    private string GetRandomFlavor()
    {
        string[] flavors = { "Fresa", "Platano", "Menta" };
        return flavors[Random.Range(0, flavors.Length)];
    }

    private void OnDestroy()
    {
        // Liberar la mesa al destruir el cliente
        if (assignedTable != null)
        {
            TableManager.instance.FreeTable(assignedTable);
        }
    }
}

