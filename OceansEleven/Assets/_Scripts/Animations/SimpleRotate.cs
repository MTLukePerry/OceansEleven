using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleRotate : MonoBehaviour {

    [SerializeField] float _xRotation = -360.0f;
    [SerializeField] float _yRotation = 0.0f;
    [SerializeField] float _zRotation = 0.0f;
    public float rotationTime = 5.0f;


	void Start () {

        StartCoroutine(RotateTheWheel());
	}

    public  IEnumerator RotateTheWheel(){
        while (this)
        {

                this.transform.DORotate(new Vector3(-_xRotation, -_yRotation, -_zRotation), rotationTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

            yield return new WaitForSeconds(rotationTime);
        }
    }

}
