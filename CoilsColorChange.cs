
using System.Net;
using UnityEngine;

public class CoilsColorChange : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startEmissionColor;
    public Color endEmissionColor;
    public bool repeatable = false;
    private float startTime;

    public GameObject Gun1Gear;
    
    // Partical Systems
    [SerializeField] private ParticleSystem _EnergyCircle = null;
    [SerializeField] private ParticleSystem _EnergyCircle2 = null;
    [SerializeField] private ParticleSystem _EnergyCircle3 = null;
    [SerializeField] private ParticleSystem _EnergySwirl = null;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
           
            //Gun1Gear.GetComponent<Transform>().transform.Rotate(new Vector3(0f, 0f, 1f)* Time.deltaTime);
            //Gun1Gear.GetComponent<Transform>().RotateAround(transform.position, transform.forward, Time.deltaTime *90f);
            //Gun1Gear.GetComponent<Transform>().transform.Rotate(Vector3.forward, Space.Self);
            //Gun1Gear.GetComponent<Transform>().transform.Rotate(Vector3.forward, Space.Self);
            _EnergyCircle.Play();
            _EnergyCircle2.Play();
            _EnergyCircle3.Play();
            float t = (Time.time - startTime) * speed;
            GetComponent<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
            GetComponent<MeshRenderer>().materials[2].SetColor("_EmissionColor",  Color.Lerp(startEmissionColor, endEmissionColor, t)); 
           
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            _EnergyCircle.Stop();
            _EnergyCircle2.Stop();
            _EnergySwirl.Play();
            _EnergyCircle3.Stop();
           
            float t = (Time.time - startTime) * speed;
            GetComponent<MeshRenderer>().materials[2].SetColor("_EmissionColor",  Color.Lerp(endEmissionColor, startEmissionColor, t));
            GetComponent<MeshRenderer>().materials[2].DisableKeyword("_EMISSION");
        }
        
    }
    
    
    //----------------------PARTICLE SYSTEMS----------------------------------------
   
//Muzzle Puff animation after pulling in a fruit
    public void MuzzleCloudParticle()
    {
        //Play the cloud particle
    
        // Play the sound effect
    }
   
// Muzzle Energy Circle
    /*public void MuzzleEnergyCircle()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Play the Energy particle
            _EnergyCircle.Play();
            _EnergyCircle2.Play();
            _EnergyCircle3.Play();
            // Play the sound effect  
        }
       
        if (Input.GetMouseButtonUp(1))
        {
            //Play the Energy particle
            _EnergyCircle.Stop();
            _EnergyCircle2.Stop();
            _EnergyCircle3.Stop();
           
           
            // Play the sound effect  
        }
       
    }*/
}
