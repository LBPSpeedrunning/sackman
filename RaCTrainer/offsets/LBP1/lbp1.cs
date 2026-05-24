using System;
using System.Collections.Generic;
using sackMAN.Memory;

namespace sackMAN.offsets.LBP1
{
    public class LBP1Addresses : IAddresses
    {
        public uint loadvalue;
        public uint connectionstatus;
        public uint slottype;
        public uint slotnumber;
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

    public class lbp1 : IGame, IAutosplitterAvailable
    {
        public lbp1(IPS3API api) : base(api)
        {
            string gameVersion = LBP1VersionForm.LBP1VersionType;
            string gameID = AttachPS3Form.game;
            if (gameVersion == "v1.21")
            {
                addr.loadvalue = 0xA07010;
                addr.connectionstatus = 0xA35EB0;
                addr.slottype = 0x98EF70;
                addr.slotnumber = 0x98EF74;
                addr.idoflevelswitch = 0x98EF48;
                addr.numofsacksspawned = 0xA7AF08;
                addr.scoreboardhit = 0x9E064C;
            }
            else if(gameVersion == "v1.30/Latest" || gameID == "NPEA00241" || gameID == "NPUA80472" || gameID == "NPJA00052" || gameID == "NPHA80092" || gameID == "BCES00611")
            {
                //adding the rest of these guys will come later SORRRRRYYYY
                //if you read this you should go into the #modding channel and say "meow"
                addr.loadvalue = 0x8C2FD4;
            }
        }

        public static LBP1Addresses addr = new LBP1Addresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.loadvalue, 4),
            (addr.connectionstatus, 4),
            (addr.slottype, 4),
            (addr.slotnumber, 4),
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
