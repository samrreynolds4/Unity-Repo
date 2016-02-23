using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour {

    public GameObject[] FireTypes = new GameObject[4];
    public GameObject[] WaterTypes = new GameObject[4];
    public GameObject[] EarthTypes = new GameObject[4];
    public GameObject[] AirTypes = new GameObject[4];

    public GameObject[][] spellBook;

    //Current Spell selected
    public SpellType spellType;
    public CastingType castType;
    public GameObject currentSpell;
    

    // Use this for initialization
    void Start()
    {
        spellBook = new GameObject[4][] { AirTypes, WaterTypes, EarthTypes, FireTypes };

        Time.timeScale = 2f;
    }

    public GameObject getSpell(SpellType spell, CastingType type)
    {
        currentSpell = spellBook[(int)spell][(int)type];
        spellType = spell;
        castType = type;
        return currentSpell;
    }
}

public enum SpellType
{
    Air = 0,
    Water = 1,
    Earth = 2,
    Fire = 3
}

public enum CastingType
{
    Charge = 0,
    Area = 1,
    Beam = 2,
    Trap = 3
}

//public class Spell : MonoBehaviour
//{
//    public CastingType type { get { return type; } set { type = value; } }
//    public SpellType spellType { get { return spellType; } set { spellType = value; } }
//    public GameObject[][] spells = new GameObject[4][];
//    GameObject spellObject { get { return spellObject; } set { spellObject = value; } }


//    public Spell()
//    { }

//    public Spell(SpellType spell)
//    {
//        type = CastingType.Charge;
//        spellType = spell;
//        spellObject = spells[(int)type][(int)spellType];
//    }

//    public Spell(SpellType spell, CastingType type)
//    {
//        spellObject = spells[(int)type][(int)spellType];
//            spellType = spell;
//        this.type = type;

//    }

//    public GameObject getSpell()
//    {
//        return spellObject;
//    }

//}