using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private GameObject[] ragdollComponents;
    [SerializeField] private float _corpsePushForce;
    private Vector3 _corpsePush;
    private Vector3 tempPos;

    private void Awake()
    {
        _corpsePush = new Vector3(0f,0f,_corpsePushForce);
    }

    public void CheckRagdollPos()
    {
        if (ragdollComponents[0] != null)
        {
            tempPos = ragdollComponents[0].transform.position;
            tempPos.y = Mathf.Clamp(tempPos.y, 0.1f, 15f);
            tempPos.z = Mathf.Clamp(tempPos.z, -5f, 15f);
            tempPos.x = Mathf.Clamp(tempPos.x, -5, 5f);
            ragdollComponents[0].transform.position = tempPos;
        }
    }

    public void SetRagdollFree()
    {
        if (ragdollComponents[0] != null)
        {
            for (int i = 0; i < ragdollComponents.Length; i++)
            {
                ragdollComponents[i].GetComponent<Collider>().isTrigger = false;
                var ragdollComponent = ragdollComponents[i].GetComponent<Rigidbody>();
                ragdollComponent.isKinematic = false;

                ragdollComponent.AddForce(_corpsePush, ForceMode.VelocityChange);

            }
        }
    }
}
