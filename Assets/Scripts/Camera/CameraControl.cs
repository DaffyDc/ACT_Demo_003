using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineFreeLook freeCam;
    public Player self;
    public Vector3 rotateAngleX;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void TipCameraZ(Vector3 angle)
    {
        self.mainCamera.Rotate(angle);
    }
}
