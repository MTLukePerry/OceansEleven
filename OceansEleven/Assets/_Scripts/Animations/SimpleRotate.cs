using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleRotate : MonoBehaviour {

    [SerializeField] float xRotation = 360.0f;
    [SerializeField] float yRotation = 0.0f;
    [SerializeField] float zRotation = 0.0f;
    [SerializeField] float rotationTime = 5.0f;
    [Tooltip("-1 is infinite")]
    [SerializeField] int numberOfRotations = -1;
    [SerializeField] bool isCounterClockwise;

	void Start () {

        if (!isCounterClockwise)
        {
            this.transform.DOLocalRotate(new Vector3(xRotation, yRotation, zRotation), rotationTime, RotateMode.LocalAxisAdd).SetLoops(numberOfRotations).SetEase(Ease.Linear);
        } else {
            this.transform.DOLocalRotate(new Vector3(-xRotation, -yRotation, -zRotation), rotationTime, RotateMode.LocalAxisAdd).SetLoops(numberOfRotations).SetEase(Ease.Linear);
        }
	}

}
