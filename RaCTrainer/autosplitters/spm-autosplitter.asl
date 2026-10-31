state("sackMAN") {}

startup
{
    settings.Add("podexitautosplit", true, "Autosplit on exiting Pod");
    settings.Add("ILreset", false, "Individual Level Autoreset");
    settings.Add("autostart", true, "Autostart");
    settings.SetToolTip("autostart", "Choose where the run autostarts at");
    settings.Add("autostartcontinue", true, "Autostart on Continue on Calibration (for Full Game)", "autostart");
    settings.SetToolTip("autostartcontinue", "Used for full game speedruns.");
    settings.Add("autostartIL", false, "Autostart on any level (for Individual Levels)", "autostart");
    settings.SetToolTip("autostartIL", "Used for Individual Level speedruns.");
    settings.Add("autoend", true, "Autoend");
    settings.SetToolTip("autoend", "Choose where the run autoends at");
    settings.Add("autoendff", true, "Autoend on Fossil Fight (for Full Game)", "autoend");
    settings.SetToolTip("autoendff", "Used for full game speedruns.");
    settings.Add("autoendIL", false, "Autoend on any level (for Individual Levels)", "autoend");
    settings.SetToolTip("autoendIL", "Used for Individual Level speedruns.");
}

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("sackman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    vars.reader.BaseStream.Position = 0;

    current.loadvalue = vars.reader.ReadUInt32();
    current.connectionstatus = vars.reader.ReadUInt32();
    current.slottype = vars.reader.ReadUInt32();
    current.slotnumber = vars.reader.ReadUInt32();
    current.idoflevelswitch = vars.reader.ReadUInt32();
    current.numofsacksspawned = vars.reader.ReadUInt32();
    current.scoreboardhit = vars.reader.ReadUInt32();
	current.SPMinputOffset = vars.reader.ReadUInt32();
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.loadvalue = vars.reader.ReadUInt32();
    current.connectionstatus = vars.reader.ReadUInt32();
    current.slottype = vars.reader.ReadUInt32();
    current.slotnumber = vars.reader.ReadUInt32();
    current.idoflevelswitch = vars.reader.ReadUInt32();
    current.numofsacksspawned = vars.reader.ReadUInt32();
    current.scoreboardhit = vars.reader.ReadUInt32();
	current.SPMinputOffset = vars.reader.ReadUInt32();
}

start
{
    if (settings["autostartcontinue"])
        if (current.slottype == 5 && current.SPMinputOffset == 64)
            return true;

    if (settings["autostartIL"])
        if (old.numofsacksspawned == 0 && current.numofsacksspawned != 0 && current.slotnumber != 0 &&
            current.idoflevelswitch != 10169)
            return true;
}

reset
{
    if (settings["ILreset"])
        if (current.idoflevelswitch != 0)
            return true;
}

split
{
    if (settings["podexitautosplit"])
        if (old.slottype != 5 && current.slottype == 5)
            return true;

    if (settings["autoendff"])
        if (current.slotnumber == 2251803992 && old.scoreboardhit == 0 && current.scoreboardhit == 4)
            return true;

    if (settings["autoendIL"])
        if (current.slotnumber != 0 && old.scoreboardhit == 0 && current.scoreboardhit == 4)
            return true;
}

isLoading
{
    if (current.slottype == 0 && current.loadvalue > 0)
        return false;
    if (current.loadvalue > 0)
        return true;
    if (current.connectionstatus == 9)
        return true;
    return false;
}