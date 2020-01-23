using System;

namespace NoonNight.Battle.RuneCanvas
{
    [Flags]
    public enum VertexType
    {
        A =  1 << 0,
        B =  A << 1,
        C =  B << 1,
        D =  C << 1,
        E =  D << 1,
        F =  E << 1,
        G =  F << 1,
    }

    [Flags]
    public enum EdgeType
    {
        AB = 1 << 0,
        AC = 1 << 1,
        AD = 1 << 2,
        AE = 1 << 3,
        AF = 1 << 4,
        AG = 1 << 5,

        BC = 1 << 6,
        BD = 1 << 7,
        BE = 1 << 8,
        BF = 1 << 9,
        BG = 1 << 10,

        CD = 1 << 11,
        CE = 1 << 12,
        CF = 1 << 13,
        CG = 1 << 14,

        DE = 1 << 15,
        DF = 1 << 16,
        DG = 1 << 17,

        EF = 1 << 18,
        EG = 1 << 19,

        FG = 1 << 20,
    }
    [Flags]
    public enum RuneType
    {
        NONE  = 0,
        Ken   = 1 << 0,
        Is    = 1 << 1,
        Beorc = 1 << 2,
        Eoh   = 1 << 3,
        Daeg  = 1 << 4,
        Yr    = 1 << 5,
        Sigel = 1 << 6,
        Lagu  = 1 << 7,
        Rad   = 1 << 8, 
        Wynn  = 1 << 9,
    }

    
    public enum EmitterPos
    {
        OnCanvas,
        OnGroundCanvas,
        OnGroundForward,
    }
}
