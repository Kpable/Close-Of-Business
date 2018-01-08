using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// The melee/ ranged weapon the player always has by default. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Bat : WeildableObject {
    
    public GameObject rockPrefab;
    public float rockDelay = 0.7f;
    private Animator anim;

    bool canThrowRock = true;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    protected override void LeftClick()
    {
        anim.Play("Bat Swing");

        //base.LeftClick();
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 4))
        {
            DestructableObject destructableObject = hit.collider.gameObject.GetComponent<DestructableObject>();
            if (destructableObject != null)
            {
                destructableObject.Damage(100);
            }
        }

    }

    protected override void RightClick()
    {
        if (canThrowRock)
        {
            canThrowRock = false;
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);

            rock.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);
            Invoke("EnableThrow", rockDelay);
        }
    }

    void EnableThrow()
    {
        canThrowRock = true;
    }
}
