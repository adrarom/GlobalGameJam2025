using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public enum CustomerState { Entering, Seated, Waiting, Served }
    public CustomerState state = CustomerState.Entering;
    [SerializeField]
    private NavMeshAgent agent;
    public string requestedFlavor;
    private Transform assignedTable;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3f;

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
            if (Vector3.Distance(transform.position, assignedTable.position) < 0.5f)
            {
                state = CustomerState.Seated;
                Debug.Log("Cliente se ha sentado.");
                RequestFlavor();
            }
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
        string[] flavors = {"Fresa", "Platano", "Menta"};
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
