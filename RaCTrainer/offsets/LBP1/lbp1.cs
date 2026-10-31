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

        public uint LBP1inputOffset;

        public uint LBP1analogOffset;
        

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
                addr.LBP1inputOffset = 0xA291A4;
                addr.LBP1analogOffset = 0xA291A9;
            }
            else if(gameVersion == "v1.30/Latest" || gameID == "NPEA00241" || gameID == "NPUA80472" || gameID == "NPJA00052" || gameID == "NPHA80092" || gameID == "BCES00611")
            {
                addr.loadvalue = 0x8C2FD4;
                addr.connectionstatus = 0x90D048;
                addr.slottype = 0x88C868;
                addr.slotnumber = 0x88C86C;
                addr.idoflevelswitch = 0x88C84C;
                addr.numofsacksspawned = 0x969B58;
                addr.scoreboardhit = 0x8C3A58;
                addr.LBP1inputOffset = 0x91F444;
                addr.LBP1analogOffset = 0x91F449;
            }
        }

        public bool HasInputDisplay => addr.LBP1inputOffset > 0 && addr.LBP1analogOffset > 0;

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
        
        protected override void SetupInputDisplayMemorySubsButtons()
        {
            int buttonMaskSubID = api.SubMemory(pid, addr.LBP1inputOffset, 4, (value) =>
            {
                Inputs.RawInputs = BitConverter.ToInt32(value, 0);
                Inputs.Mask = Inputs.DecodeMask(Inputs.RawInputs);
            });
        }

        public float ConvertStickFormat(byte value)
        {
            return (value - 128) / 127.0f;
        }

        protected override void SetupInputDisplayMemorySubsAnalogs()
        {
            int analogRSubID = api.SubMemory(pid, addr.LBP1analogOffset, 4, (value) =>
            {
                Inputs.ry = ConvertStickFormat(value[1]);
                Inputs.rx = ConvertStickFormat(value[3]);
            });

            int analogYSubID = api.SubMemory(pid, addr.LBP1analogOffset + 4, 4, (value) =>
            {
                Inputs.ly = ConvertStickFormat(value[1]);
                Inputs.lx = ConvertStickFormat(value[3]);
            });
        }

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
