using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpellUI : MonoBehaviour {

    public Text SpellText;

    private string spell;
    private string type;

    //gameOver text
    public Text gameOverText;

    public void SetSpell(SpellType spell, CastingType type )
    {
        switch(spell)
        {
            case SpellType.Air:
                this.spell = "Air";
                break;
            case SpellType.Earth:
                this.spell = "Earth";
                break;
            case SpellType.Fire:
                this.spell = "Fire";
                break;
            case SpellType.Water:
                this.spell = "Water";
                break;
        }

        switch (type)
        {
            case CastingType.Charge:
                this.type = "Charge";
                break;
            case CastingType.Beam:
                this.type = "Beam";
                break;
            case CastingType.Area:
                this.type = "Area";
                break;
            case CastingType.Trap:
                this.type = "Trap";
                break;
        }
        Debug.Log(spell + " " + type);
        SpellText.text = "Spell: " + this.spell + "\n" +
                         "Type:  " + this.type;
    }

    public void GameOver()
    {
        gameOverText.enabled = true;
    }
}
