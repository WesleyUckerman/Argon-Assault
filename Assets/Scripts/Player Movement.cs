using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Setings")]
    [Tooltip("How fast your ship moves based upon player ship inputs")]
    [SerializeField] float Controlspeed = 0f;
    [SerializeField] float Xrange = 0f;
    [SerializeField] float YrangeUp = 0f;
    [SerializeField] float YrangeDown = 0f;

    [Header("Screen position based on tuning")]
    [SerializeField] float PositionYawFactor = 0f;
    [SerializeField] float PositionPitchFactor = 0f;
    
    [Header("Screen position based on tuning")]
    [SerializeField] float ControlPitchFactor = 0f;
    [SerializeField] float ControlRollFactor = 0f;

    [Header("Laser gun Array")]
    [Tooltip("Add all Lasers here")]
    [SerializeField] GameObject[] Lasers;




    float XThrow,YThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float PitchDueToPosition = transform.localPosition.y * PositionPitchFactor;
        float PitchDueToControlThrow = YThrow * ControlPitchFactor;
        float Pitch = PitchDueToPosition + PitchDueToControlThrow ;

        float YawDueToPosition = transform.localPosition.x * PositionYawFactor;
        float Yaw = YawDueToPosition;
        float RollDueToControlFactor = XThrow * ControlRollFactor;
        float Roll = RollDueToControlFactor;

        transform.localRotation = Quaternion.Euler(Pitch,Yaw,Roll);
    }

    private void ProcessTranslation()
    {
        XThrow = Input.GetAxis("Horizontal");
        float Xoffset = XThrow * Time.deltaTime * Controlspeed;
        float RawXPos = transform.localPosition.x + Xoffset;
        float Xclamp = Mathf.Clamp(RawXPos, -Xrange, Xrange);

        YThrow = Input.GetAxis("Vertical");
        float Yoffset = YThrow * Time.deltaTime * Controlspeed;
        float RawYPos = transform.localPosition.y + Yoffset;
        float YClamp = Mathf.Clamp(RawYPos, -YrangeDown, YrangeUp);

        transform.localPosition = new Vector3(Xclamp, YClamp, transform.localPosition.z);
    }

    void ProcessFiring ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SetActiveLasers(true);
        }

        else
        {
            SetActiveLasers(false);
        }
    }

    void SetActiveLasers (bool IsActive = true)
    {
        foreach (GameObject Laser in Lasers)
        {
            var emissionmodule = Laser.GetComponent<ParticleSystem>().emission;
            emissionmodule.enabled = IsActive;
        }
    }

}
