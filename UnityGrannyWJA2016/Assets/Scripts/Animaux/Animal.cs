using UnityEngine;
using System.Collections;

public abstract class Animal : MonoBehaviour {


    [Tooltip("0 si Carnivore; 1 si Herbivore.")]
    protected bool type;
    [Tooltip("Le nom de la classe de l'animal. Ex: Lion.")]
    protected string name;
    [Tooltip("1 si l'animal mange.")]
    protected bool isEating;
    [Tooltip("1 si l'animal est coupler.")]
    protected bool couple;
    [Tooltip("represente le pourcentage de chance que l'animal sorte de la cage.")]
    protected float goingOutChance;
    [Tooltip("1 si le joueur tien l'animal dans ces bras.")]
    protected bool grabed;
    [Tooltip("0 = Mauve, 1 = Orange, 2 = Vert.")]
    protected int color;
    [Tooltip("0 si dans le spawn; 1 si sur le bateau; 2 si dans une cage")]
    protected int zone;

    public void setType(bool t) { type = t; }
    public void setName(string n) { name = n; }
    public void setIsEating(bool i) { isEating = i; }
    public void setCouple(bool c) { couple = c; }
    public void setGoingOutChance(float g) { goingOutChance = g; }
    public void setColor(int c) { color = c; }
    public void setgrabed(bool g) { grabed = g; }
    public void setZone(int z) { zone = z; }

    public bool getType() { return type; }
    public string getName() { return name; }
    public bool getIsEating() { return isEating; }
    public bool getCouple() { return couple; }
    public float getGoingOutChance() { return goingOutChance; }
    public int getColor() { return color; }
    public bool getgrabed() { return grabed; }
    public int getZone() { return zone; }

}
