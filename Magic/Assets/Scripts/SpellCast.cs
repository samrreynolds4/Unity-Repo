using UnityEngine;
using System.Collections;

public enum SpellCodes
{
    //Spells
    Air = KeyCode.A,
    Water = KeyCode.S,
    Earth = KeyCode.D,
    Fire = KeyCode.F,

    //Types
    Charge = KeyCode.Q,
    Beam = KeyCode.W,
    Trap = KeyCode.E,
    Area = KeyCode.R
}

public class SpellCast : MonoBehaviour {


    /*     
    Air = 0,
    Water = 1,
    Earth = 2,
    Fire = 3
    
    Charge = 0,
    Area = 1,
    Beam = 2,
    Trap = 3
        
    */


    public CastingType type;
    public SpellType spell;
    public SpellController spellControl;
    public SpellUI spellUI;

    public Material[] BeamMaterial; //Beam materials to use when casting a beam
    public ParticleSystem[] BeamParticles; //Beam particles when casting a beam spell
    public LineRenderer Beam;   //Beam for beam spell

    Ray shootRay;   //Find where the beam will hit
    RaycastHit shootHit;

    //indexes for LineRenderer
    private int START = 0;
    private int END = 1;
    

    void Start ()
    {
        type = CastingType.Charge;
        spell = SpellType.Fire;
        Beam = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.anyKeyDown)
        {
            SwitchSpells();
        }
	}

    //Switch the spells if one of the keys is pressed
    void SwitchSpells()
    {

        // Setting Spell Type
        if (keyDown(SpellCodes.Air))
        {
            spell = SpellType.Air;
        }

        if (keyDown(SpellCodes.Earth))
        {
            spell = SpellType.Earth;
        }

        if (keyDown(SpellCodes.Water))
        {
            spell = SpellType.Water;
        }

        if (keyDown(SpellCodes.Fire))
        {
            spell = SpellType.Fire;
        }

        //Setting Casting Type
        if (keyDown(SpellCodes.Charge))
        {
            type = CastingType.Charge;
        }

        if (keyDown(SpellCodes.Beam))
        {
            type = CastingType.Beam;
        }

        if (keyDown(SpellCodes.Area))
        {
            type = CastingType.Area;
        }

        if(keyDown(SpellCodes.Trap))
        {
            type = CastingType.Trap;
        }

        Debug.Log("Spell: " + spell + " Type: " + type);
        SetSpell(spell, type);
        DisableAllBeamEffects();
    }

    bool keyDown(SpellCodes key)
    {
        bool value = false;

        if (Input.GetKeyDown((KeyCode)key))
            value = true;

        return value;
    }

    void SetSpell(SpellType spell, CastingType type)
    {
        this.spell = spell;
        this.type = type;
        spellUI.SetSpell(spell, type);
    }

    public void Cast ()
    {
        Instantiate(spellControl.getSpell(spell, type), transform.position + transform.forward*5, Quaternion.identity);
    }

    //Casting a Beam!
    public void BeamCast()
    {
        //Enable the beam and set material and pos
        Beam.enabled = true;
        Beam.SetPosition(START, transform.position);
        Beam.material = BeamMaterial[(int)spell];

        //Make the texture move
        textureOffset(BeamMaterial[(int)spell], 4f);
        
        //Where we are shooting from
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        BeamParticles[(int)spell].gameObject.SetActive(true);

        //Find where the beam hits
        if (Physics.Raycast(shootRay, out shootHit))
        {
            Beam.SetPosition(END, shootHit.transform.position);

            //Sets the particle lifetime so particles will die after it passes the point
            BeamParticles[(int)spell].startLifetime = (transform.position - shootHit.point).magnitude * BeamParticles[(int)spell].startSpeed/10;
        }
    }

    public void textureOffset(Material mat, float speed)
    {
        float y = Mathf.Repeat(Time.time * speed, 1);
        Vector2 offset = new Vector2(y, 1);
        mat.mainTextureOffset = offset;
    }

    //Disable Beam and particle system
    public void BeamDisable()
    {
        Beam.enabled = false;
        BeamParticles[(int)spell].gameObject.SetActive(false);

    }

    //Disable all beam effects when switching spells
    public void DisableAllBeamEffects()
    {
        BeamParticles[0].gameObject.SetActive(false);
        BeamParticles[1].gameObject.SetActive(false);
        BeamParticles[2].gameObject.SetActive(false);
        BeamParticles[3].gameObject.SetActive(false);
        BeamDisable();

    }

    //cast a charge spell!
    public void ChargeCast(float n)
    {
        spellControl.getSpell(spell, type).GetComponent<chargingSpeed>().setSpeed(transform.forward, n);
        Instantiate(spellControl.getSpell(spell, type), transform.position, Quaternion.LookRotation(transform.forward));
    }
}


