using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GamePiece parent;
    public LineRenderer lineBody;
    public LineRenderer lineFill;

    [SerializeField]
    private int numSegments;
    [SerializeField]
    private float initAngle;
    [SerializeField]
    private float incrementDivisor;
    [SerializeField]
    private float widthMultiplier;
    [SerializeField]
    private float deltaY;
    [SerializeField]
    private float deltaX;


    // Start is called before the first frame update
    /*
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0, deltaY, 0);

        lineBody.widthMultiplier = widthMultiplier;
        lineFill.widthMultiplier = widthMultiplier;

        float angleIncrement = Mathf.PI / numSegments / incrementDivisor;
        float angle = initAngle;
        Vector3[] positions = new Vector3[numSegments + 1];
        for (var i = 0; i <= numSegments; i++)
        {
            positions[i] = new Vector3(
                Mathf.Cos(angle) + deltaX,
                0,
                Mathf.Sin(angle)
            );
            angle += angleIncrement;
        }

        lineBody.positionCount = numSegments + 1;
        lineBody.SetPositions(positions);

        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        // calculate the positions of the points
        float baseAngleIncrement = Mathf.PI / numSegments / incrementDivisor;
        float angleIncrement = baseAngleIncrement * (((float)parent.health)/parent.maxHealth);
        float angle = initAngle;
        angle += baseAngleIncrement * numSegments;
        Vector3[] positions = new Vector3[numSegments + 1];
        for (var i = 0; i <= numSegments; i++)
        {
            positions[i] = new Vector3(
                Mathf.Cos(angle) + deltaX,
                0,
                Mathf.Sin(angle)
            );
            angle -= angleIncrement;
        }

        lineFill.positionCount = numSegments + 1;
        lineFill.SetPositions(positions);
    }*/
}
