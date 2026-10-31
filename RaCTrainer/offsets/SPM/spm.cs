using System;
using System.Collections.Generic;
using sackMAN.Memory;

namespace sackMAN.offsets.SPM
{
    public class SPMAddresses : IAddresses
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

        public uint SPMinputOffset;

        public uint SPManalogOffset;
        

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => throw new NotImplementedException();

        public uint mobyInstances => throw new NotImplementedException();
    }

    public class spm : IGame, IAutosplitterAvailable
    {
        public spm(IPS3API api) : base(api)
        {
                addr.loadvalue = 0x8C2DF4;
                addr.connectionstatus = 0x90CCD8;
                addr.slottype = 0x88C670;
                addr.slotnumber = 0x88C674;
                addr.idoflevelswitch = 0x88C650;
                addr.numofsacksspawned = 0x96A758;
                addr.scoreboardhit = 0x8C36B8;
                addr.SPMinputOffset = 0x91FFA4;
                addr.SPManalogOffset = 0x91FFA9;
        }

        public bool HasInputDisplay => addr.SPMinputOffset > 0 && addr.SPManalogOffset > 0;

        public static SPMAddresses addr = new SPMAddresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.loadvalue, 4),
            (addr.connectionstatus, 4),
            (addr.slottype, 4),
            (addr.slotnumber, 4),
            (addr.idoflevelswitch, 4),
            (addr.numofsacksspawned, 4),
            (addr.scoreboardhit, 4),
            (addr.SPMinputOffset, 4),
        };
        
        protected override void SetupInputDisplayMemorySubsButtons()
        {
            int buttonMaskSubID = api.SubMemory(pid, addr.SPMinputOffset, 4, (value) =>
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
            int analogRSubID = api.SubMemory(pid, addr.SPManalogOffset, 4, (value) =>
            {
                Inputs.ry = ConvertStickFormat(value[1]);
                Inputs.rx = ConvertStickFormat(value[3]);
            });

            int analogYSubID = api.SubMemory(pid, addr.SPManalogOffset + 4, 4, (value) =>
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
