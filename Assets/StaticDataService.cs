using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StaticDataService
{
    private const string VladDataPath = "StaticData/SkinsData/Vlad";
    private const string GlentDataPath = "StaticData/SkinsData/Glent";
    private Dictionary<int, SkinData> _vladSkins;
    private Dictionary<int, SkinData> _glentSkins;

    public void LoadVladSkins()
    {
        _vladSkins = Resources
            .LoadAll<SkinData>(VladDataPath)
            .ToDictionary(x => x.ID, x => x);
    }
    
    
    public void LoadGlentSkins()
    {
        _glentSkins = Resources
            .LoadAll<SkinData>(GlentDataPath)
            .ToDictionary(x => x.ID, x => x);
    }

    public SkinData ForSkin(SkinTypeID skinTypeID, int number)
    {
        switch (skinTypeID)
        {
            case (SkinTypeID.Vlad):
            {
                return _vladSkins.TryGetValue(number, out SkinData staticData)
                    ? staticData
                    : null;
            }
            case(SkinTypeID.Glent):
            {
                return  _glentSkins.TryGetValue(number, out SkinData staticData)
                    ? staticData 
                    : null;
            }
        }
        return null;
    }

    public int SkinAmount(SkinTypeID typeID)
    {
        switch (typeID)
        {
            case (SkinTypeID.Vlad):
                return _vladSkins.Count;
            case (SkinTypeID.Glent):
                return _glentSkins.Count;
        }

        return -1;
    }

}
