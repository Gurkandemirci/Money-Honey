using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortTypes
{
    ClothesPort,
    JobPort
}

public enum Jobs
{
    Doctor,
    Firefighter,
    Homeless,
    Judge,
    Millionare,
    Police
}

public enum ClothesEnum
{
    Hat,
    Hat2,
    Shirt,
    Shirt2,
    Shirt3,
    Shirt4,
    Shirt5,
    Shirt6,
    Short,
    Short2,
    Short3,
    Short4,
    Short5,
    Short6,
    Short7,
    Short8,
    Short9,
    Short10,
    Shose,
    Shose2,
    Shose3,
    Shose4,
    Shose5,
    Shose6,
    Hats,
    Shirts,
    Shorts,
    Shoses
}
public class EnumManager : MonoBehaviour
{
    public ClothesEnum myPart;
    public ClothesEnum myClothes;
    public PortTypes myPortType;

}
