using System;
using System.Collections.Generic;
using sackMAN.Memory;

namespace sackMAN.offsets.LBP2
{
    public class LBP2Addresses : IAddresses
    {
        public uint loadvalue1;
        public uint loadvalue2;
        public uint loadvalue3;
        public uint slotnumber;
        public uint idselectedonplanet;
        public uint idoflevelswitch;
        public uint numofsacksspawned;
        public uint scoreboardhit;

        public uint boltCount => throw new NotImplementedException();

        public uint playerCoords => throw new NotImplementedException();

        public uint inputOffset => throw new NotImplementedException();

        public uint analogOffset => throw new NotImplementedException();

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => throw new NotImplementedException();

        public uint mobyInstances => throw new NotImplementedException();
    }

    public class lbp2 : IGame, IAutosplitterAvailable
    {
        public lbp2(IPS3API api) : base(api)
        {
            string gameVersion = LBP2VersionForm.LBP2VersionType;
            string gameID = AttachPS3Form.game;
            if (gameVersion == "v1.00/Unpatched")
            {
                // getting removed next update to be v1.01 instead ENJOY IT WHILE IT LASTS
                addr.loadvalue1 = 0xC8DDB8;
                addr.loadvalue2 = 0xD05658;
                // 0xD05668 for loadvalue2 also works too
                // 0xCF8E6C is game freeze value
                addr.loadvalue3 = 0xD08408;
            }
            else if (gameVersion == "v1.33/Latest" || gameID == "NPUA80662" || gameID == "NPEA00324" || gameID == "BCUS98372" || gameID == "BCES01693" || gameID == "BCES01694" || gameID == "NPEA00437")
            {
                addr.loadvalue1 = 0xDC765C;
                // other addresses for loadvalue1 are 0xDC7668 and OxE249D4
                addr.loadvalue2 = 0xE3F6A8;
                // 0xE3F6B8 for loadvalue2 also works too
                // 0xE326AC is game freeze value
                addr.loadvalue3 = 0xE430B8;
                addr.slotnumber = 0xDF761C;
                addr.idselectedonplanet = 0xDF66DC;
                addr.idoflevelswitch = 0xE4BA60;
                addr.numofsacksspawned = 0xE9BD98;
                addr.scoreboardhit = 0xE1C5B8;
            }
        }

        public static LBP2Addresses addr = new LBP2Addresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.loadvalue1, 4),
            (addr.loadvalue2, 4),
            (addr.loadvalue3, 4),
            (addr.slotnumber, 4),
            (addr.idselectedonplanet, 4),
            (addr.idoflevelswitch, 4),
            (addr.numofsacksspawned, 4),
            (addr.scoreboardhit, 4),
        };

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }
    }
}
