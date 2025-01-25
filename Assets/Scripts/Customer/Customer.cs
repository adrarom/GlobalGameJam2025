using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public enum CustomerState { Entering, Seated, Waiting, Served }
    public CustomerState state = CustomerState.Entering;
    [SerializeField]
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    public string requestedFlavor;
    private Transform assignedTable;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent.speed = 3f;
        obstacle.enabled = false; // Deshabilitar el obstáculo al inicio
        Transform availableTableTransform = TableManager.instance.GetAvailableTable();
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
                Debug.Log("Cliente se ha sentado.");
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
            obstacle.enabled = true; // Habilitar el obstáculo cuando el cliente esté sentado
            gameObject.transform.position = table.position;
        }
    }

    private void AssignTable(Transform tablePosition)
    {
        assignedTable = tablePosition;
        agent.SetDestination(assignedTable.position);
    }

    private void RequestFlavor()
    {
        requestedFlavor = GetRandomFlavor();
        Debug.Log("Cliente pide sabor: " + requestedFlavor);
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
