using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //flag to check if the user has tapped / clicked. 
    //Set to true on click. Reset to false on reaching destination
    private bool flag = false;
   
    //destination point
    private Vector3 endPoint;
    private Vector3 lookPoint;
    
    //alter this to change the speed of the movement of player / gameobject
    public float duration = 50.0f;
   
    //vertical position of the gameobject
    private float yAxis;
   
    //Spell Casting Object
    public SpellCast casting;
   
    //Spell charging effects
    public ParticleSystem[] holdingEffects;
    
    //Floor mask for choosing where new position will be
    int floorMask;

    //Delay for area Attack
    public float AreaDelay = 4f;

    //Can do an area attack?
    public bool areaAttack = true;

    //Charging factor for charging spell
    private float chargeFactor = 3f;

    //Check to see if the left mouse button was clicked
    private bool didClick = false;

    private Quaternion lookRotation;
    private Vector3 direction;
    private Vector3 lookDirection;
    public float rotationSpeed;

    void Start()
    {
        //save the y axis value of gameobject
        yAxis = gameObject.transform.position.y;
        floorMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {

        if (casting.type == CastingType.Charge)
        {
            if (Input.GetMouseButton(0))
            {
                didClick = true;
                chargeFactor += Time.deltaTime*2;
                holdingEffects[(int)casting.spell].loop = true;
                if(!holdingEffects[(int)casting.spell].isPlaying)
                {
                    holdingEffects[(int)casting.spell].enableEmission = true;
                    holdingEffects[(int)casting.spell].Play();
                }
                aimCast();

                holdingEffects[(int)casting.spell].emissionRate = chargeFactor * 2;
                
            }
            else
            {
                if(didClick)
                {
                    didClick = false;
                    holdingEffects[(int)casting.spell].enableEmission = false;
                    holdingEffects[(int)casting.spell].Stop();
                    casting.ChargeCast(chargeFactor);
                    chargeFactor = 3f;
                }
            }

            

        } else
        {
            if (casting.type == CastingType.Area )
            {
                if(areaAttack && Input.GetMouseButtonDown(0))
                {
                    casting.Cast();
                    areaAttack = false;
                    StartCoroutine(Delay(AreaDelay));
                }
            } else
            {
                if (casting.type == CastingType.Beam)
                {
                    if(Input.GetMouseButton(0))
                    {
                        casting.BeamCast();
                        aimCast();
                    }
                    else { casting.BeamDisable(); }
                    
                }
            }
        }


        //check if the screen is touched / clicked   
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButton(1)))
        {
            //declare a variable of RaycastHit struct
            RaycastHit hit;
            //Create a Ray on the tapped / clicked position
            Ray ray;
            //for unity editor
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //for touch device


            //Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit, floorMask))
            {
                //set a flag to indicate to move the gameobject
                flag = true;
                //save the click / tap position
                endPoint = hit.point;
                //as we do not want to change the y axis value based on touch position, reset it to original y axis value
                endPoint.y = yAxis;
                //Debug.Log(endPoint);
            }

        }
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        { //&& !(V3Equal(transform.position, endPoint))){
            direction = (endPoint - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
          //move the gameobject to the desired position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            flag = false;
            //Debug.Log("I am here");
        }

    }

    public IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        areaAttack = true;
    }

    public void aimCast()
    {
        RaycastHit hit;
        //Create a Ray on the tapped / clicked position
        Ray ray2;
        //for unity editor
        ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray2, out hit, floorMask))
        {
            //save the click / tap position
            lookPoint = hit.point;
            //as we do not want to change the y axis value based on touch position, reset it to original y axis value
            lookPoint.y = yAxis;
            //Debug.Log(endPoint);
        }

        lookDirection = (lookPoint - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
    }

}
