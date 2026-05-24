state("sackMAN") {}

startup
{    
	settings.Add("autosplit", true, "Autosplit");
	settings.SetToolTip("autosplit", "Choose where the run autosplits at");
	settings.Add("podexitautosplit", true, "Autosplit on exiting Pod", "autosplit");
	settings.Add("podtransautosplit", false, "Autosplit during Pod transition", "autosplit");
	settings.Add("autostart", true, "Autostart");
	settings.SetToolTip("autostart", "Choose where the run autostarts at");
	settings.Add("autostartIL", false, "Autostart on any level (for Individual Levels)", "autostart");
	settings.Add("autoend", true, "Autoend");
	settings.SetToolTip("autoend", "Choose where the run autoends at");
	settings.Add("autoendfinalboss", true, "Autoend on Final Boss Scoreboard", "autoend");
	settings.SetToolTip("autoendfinalboss", "Used for categories that end on the final boss.");
	settings.Add("autoendfinalcs", false, "Autoend on skipping Homeward Bound", "autoend");
	settings.SetToolTip("autoendfinalcs", "Used for categories that need to watch the final cutscene.");
	settings.Add("autoendIL", false, "Autoend on any level (for Individual Levels)", "autoend");
}

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("sackman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.LV1 = vars.reader.ReadUInt32();
    current.LV2 = vars.reader.ReadUInt32();
    current.LV3 = vars.reader.ReadUInt32();
	current.slotnumber = vars.reader.ReadUInt32();
	current.idselectedonplanet = vars.reader.ReadUInt32();
	current.idoflevelswitch = vars.reader.ReadUInt32();
	current.numofsacksspawned = vars.reader.ReadUInt32();
	current.scoreboardhit = vars.reader.ReadUInt32();

    vars.disableLV1 = false;
	
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.LV1 = vars.reader.ReadUInt32();
    current.LV2 = vars.reader.ReadUInt32();
    current.LV3 = vars.reader.ReadUInt32();
	current.slotnumber = vars.reader.ReadUInt32();
	current.idselectedonplanet = vars.reader.ReadUInt32();
	current.idoflevelswitch = vars.reader.ReadUInt32();
	current.numofsacksspawned = vars.reader.ReadUInt32();
	current.scoreboardhit = vars.reader.ReadUInt32();

    if (current.LV1 > 0 && current.LV2 == 1) {
        vars.disableLV1 = true;
    }

    if (current.LV1 == 0 && current.LV2 == 0) {
        vars.disableLV1 = false;
    }
	
}

start
{
	if (settings["autostartIL"])
	{
		if ((old.numofsacksspawned == 0 && current.numofsacksspawned != 0) && current.slotnumber != 0 && current.idoflevelswitch != 10169)
		{
			return true;
		}		
	}
}

reset
{

}

split
{
	if (settings["podexitautosplit"])
	{
		if (old.idoflevelswitch == 0 && current.idoflevelswitch == 10169)
		{
			return true;
		}
	}
	
	if (settings["podtransautosplit"])
	{
		if (old.idoflevelswitch == 10169 && current.idoflevelswitch == 0)
		{
			return true;
		}
	}
	
	if (settings["autoendfinalboss"])
	{
		if (current.idselectedonplanet == 152889 && old.scoreboardhit == 0 && current.scoreboardhit == 4)
		{
			return true;
		}
	}
	
	if (settings["autoendfinalcs"])
	{
		if (current.idselectedonplanet == 127497 && old.idoflevelswitch == 0 && current.idoflevelswitch == 10169)
		{
			return true;
		}
	}
	
	if (settings["autoendIL"])
	{
		if (old.scoreboardhit == 0 && current.scoreboardhit == 4)
		{
			return true;
		}
	}
}

isLoading 
{
    
    return (current.LV1 > 0 && !vars.disableLV1) || (current.LV2 > 0) || (current.LV3 > 0);
}