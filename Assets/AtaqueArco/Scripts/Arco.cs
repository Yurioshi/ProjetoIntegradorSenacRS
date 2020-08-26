using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public Transform arrowPosition;
    public Transform lineTransform;
    public GameObject arrowPrefab;
    public int pushTime;
    public int arrowQuantity; 
    public float shootForce;
    public Flecha currentArrow;
    bool hasArrow = false;
    bool bowIsReady = true;
    int interpolationFramesCount;
    int elapsedFrames = 0;
    readonly Vector3 lineInitialState = new Vector3(0f, 0f, 0.17f);
    readonly Vector3 lineMaxState = new Vector3(0f, 0f, 1f);

    private void Awake()
    {
        SetinterpolationFramesCount(pushTime);
    }

    private void Update()
    {
        if(hasArrow && currentArrow)
        {
            if (Input.GetMouseButton(0) && shootForce <= 1f)
            {
                float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
                ChargeShoot(interpolationRatio);
                elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
            }
            else if (!Input.GetMouseButton(0) && shootForce > 0f)
            {
                Shoot();
            }
        }
        else if(Input.GetMouseButtonDown(1) && !hasArrow && !currentArrow && bowIsReady)
        {
            RechargeBow();
        }
    }

    void RechargeBow()
    {
        currentArrow = Instantiate(arrowPrefab, arrowPosition).GetComponent<Flecha>();
        
        if (currentArrow) 
        {
            currentArrow.transform.parent = lineTransform;
            hasArrow = true; 
        }
    }

    void ChargeShoot(float shootTime)
    {
        lineTransform.localPosition = Vector3.Lerp(lineInitialState, lineMaxState, shootTime);
        shootForce = Mathf.Lerp(0f, 1.02f, shootTime);
    }
    
    void Shoot()
    {
        bowIsReady = false;
        hasArrow = false;
        currentArrow.transform.parent = null;
        Vector3 shootFinalForce = (lineTransform.TransformDirection(Vector3.forward) * shootForce) * 25f;
        currentArrow.rb.AddForce(-shootFinalForce, ForceMode.Impulse);
        currentArrow.StartInAirBehaviour();
        currentArrow = null;
        StartCoroutine(RetriveBowLine());
    }

    IEnumerator RetriveBowLine()
    {
        SetinterpolationFramesCount(2);
        while(shootForce > 0)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            lineTransform.localPosition = Vector3.Lerp(lineMaxState, lineInitialState, interpolationRatio);
            shootForce = Mathf.Lerp(1.003f, 0f, interpolationRatio);
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
            yield return new WaitForEndOfFrame();
        }
        SetinterpolationFramesCount(pushTime);
        bowIsReady = true;
    }

    void SetinterpolationFramesCount(int value)
    {
        interpolationFramesCount = value * 15;
    }
}

