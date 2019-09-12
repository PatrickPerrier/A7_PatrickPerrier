using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    float maxDistance;

    [SerializeField]
    ParticleSystem mParticle;

    [SerializeField]
    ParticleSystem aParticle;


    private ObjectInfo selectedInfo;
    private NavMeshAgent agent;

    public List<GameObject> allSelectedUnits;
    public GameObject selectedUnit;

    RaycastHit hit;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
            Instantiate(mParticle, hit.point, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(1) && selectedInfo.isSelected)
        {
            RightClick();
        }

        foreach (GameObject units in allSelectedUnits)
        {
            if (units.GetComponent<ObjectInfo>().health <= 0)
            {
                
                allSelectedUnits.Remove(units);
                print(units.GetComponent<ObjectInfo>().objectName + " has been killed");
            }
            else
            agent = units.GetComponent<NavMeshAgent>();
            units.GetComponent<ObjectInfo>().CheckAttackRange();
        }
        
    }
    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                selectedUnit = null;
                allSelectedUnits.Clear();
                foreach (GameObject units in allSelectedUnits)
                {
                    Debug.Log("Foreach");
                    units.GetComponent<ObjectInfo>().isSelected = false;
                }
                
                Debug.Log("Deselected");
            }
            else if (hit.collider.tag == "Selectable" && hit.collider.GetComponent<ObjectInfo>().isEnemy == false)
            {
                selectedUnit = hit.collider.gameObject;
                if (allSelectedUnits.Contains(selectedUnit))
                {
                    allSelectedUnits.Remove(selectedUnit);
                }
                else if (allSelectedUnits.Contains(selectedUnit) == false)
                {
                    allSelectedUnits.Add(selectedUnit);
                }
                
                selectedInfo = selectedUnit.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;

                Debug.Log("Selected " + selectedInfo.objectName);
            }
            else if (hit.collider.tag == "Selectable" && allSelectedUnits.Contains(selectedUnit) == true)
            {
                allSelectedUnits.Remove(selectedUnit);
            }
        }
    }
    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {

            if (hit.collider.tag == "Ground")
            {
                foreach (GameObject unit in allSelectedUnits )
                {
                    agent = unit.GetComponent<NavMeshAgent>();

                    unit.GetComponent<ObjectInfo>().groundMove = true;
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                    Debug.Log(hit.point);
                    unit.GetComponent<ObjectInfo>().hasTarget = false;
                    
                }
               
            }
            else if (hit.collider.GetComponent<ObjectInfo>().isEnemy)
            {
                GameObject targetObject = hit.collider.gameObject;
                foreach (GameObject unit in allSelectedUnits)
                {
                    agent = unit.GetComponent<NavMeshAgent>();
                    unit.GetComponent<ObjectInfo>().groundMove = false;
                    agent.SetDestination(targetObject.transform.position);
                    unit.GetComponent<ObjectInfo>().hasTarget = true;
                    unit.GetComponent<ObjectInfo>().target = hit.collider.gameObject;
                    unit.GetComponent<ObjectInfo>().isTDead = false;
                }
            }
        }
    }
    
    
    private void OnDrawGizmos()
    {

        foreach (GameObject g in allSelectedUnits)
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(g.transform.position, g.transform.up, 1f);
        }
    }
}
