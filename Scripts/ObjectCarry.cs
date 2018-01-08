using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// The player's ability to pick up an object
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ObjectCarry : MonoBehaviour
{

    public float force = 10;
    public GameObject objectHolder;
    public float rayDistance = 10f;
    public GameObject bat;
    public int damageOnHit = 100;

    private Camera fpCamera;
    private Ray ray;
    private RaycastHit hit;
    private GameObject hitObject = null;
    private GameObject heldObject = null;
    

    // Use this for initialization
    void Awake()
    {
        fpCamera = GetComponentInChildren<Camera>();
    }

    private void OnDrawGizmos()
    {
        
        if(fpCamera) Gizmos.DrawLine(fpCamera.transform.position, new Vector3(fpCamera.transform.position.x, fpCamera.transform.position.y, fpCamera.transform.position.z + rayDistance));
    }

    // Update is called once per frame
    void Update()
    {
        //Get Object
        if (Input.GetKeyDown(KeyCode.E))
        {
            // drop current object 
            if (heldObject)
            {
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody>().isKinematic = false;

                if (heldObject.GetComponent<Collider>())
                    heldObject.GetComponent<Collider>().enabled = true;

                heldObject = null;
                MissionManager.heldObjectTag = "";
                bat.SetActive(true);


            }
            else
            {
                ray = fpCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    hitObject = hit.collider.gameObject;
                    if (hitObject && hitObject.GetComponent<Rigidbody>())
                    {
                        bat.SetActive(false);

                        heldObject = hitObject;
                        MissionManager.heldObjectTag = heldObject.tag;
                        if (heldObject.GetComponent<Collider>()) heldObject.GetComponent<Collider>().enabled = false;
                        heldObject.transform.SetParent(objectHolder.transform);
                        heldObject.transform.position = objectHolder.transform.position;
                        heldObject.transform.rotation = Quaternion.Euler(Vector3.zero);

                        if (heldObject.GetComponent<WeildableObject>())
                            heldObject.GetComponent<WeildableObject>().enabled = true;

                    }

                    heldObject.GetComponent<Rigidbody>().isKinematic = true;

                    //Debug.DrawLine(ray.origin, hit.point, Color.cyan, 3f);
                }
                Debug.DrawLine(ray.origin, hit.point, Color.cyan, 3f);

            }
        }

        if (!Input.GetMouseButton(0) && Input.GetMouseButtonDown(1))
        {
            //ray = fpCamera.ScreenPointToRay(Input.mousePosition);

            // If holding throw
            if (heldObject)
            {
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.transform.SetParent(null);

                heldObject.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * force, ForceMode.VelocityChange);
                if (heldObject.GetComponent<Collider>()) heldObject.GetComponent<Collider>().enabled = true;

                heldObject = null;
                MissionManager.heldObjectTag = "";

                bat.SetActive(true);
            }

        }
    }
}
