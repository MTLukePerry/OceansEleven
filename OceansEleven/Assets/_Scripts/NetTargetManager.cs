using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetTargetManager : MonoBehaviour
{
    public float pickupTime = 2;

    private List<Transform> spawnPoints = new List<Transform>();

    private void Awake()
    {
        SingletonManager.RegisterSingleton(this);

        spawnPoints = GetComponentsInChildren<Transform>().ToList();
    }

    public void DropOnBoat(GameObject obj)
    {
        StartCoroutine(DropOnBoatRoutine(obj));
    }

    private IEnumerator DropOnBoatRoutine(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        var col = obj.GetComponent<Collider>();
        if (rb != null && col != null)
        {
            rb.isKinematic = true;

            var rtw = obj.GetComponent<RideTheWave>();
            if (rtw != null)
            {
                Destroy(rtw);
            }

            var randomTarget = spawnPoints[(new System.Random()).Next(0, spawnPoints.Count)];
            var initialTransform = obj.transform;

            float lerpTime = 0;
            while (lerpTime < pickupTime)
            {
                lerpTime += Time.deltaTime;
                lerpTime = Mathf.Min(pickupTime, lerpTime);

                obj.transform.position = Vector3.Lerp(initialTransform.position, randomTarget.position, lerpTime / pickupTime);
                yield return null;
            }

            rb.isKinematic = false;
            col.isTrigger = false;
        }
    }
}
