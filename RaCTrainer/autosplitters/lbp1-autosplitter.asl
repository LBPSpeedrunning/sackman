state("sackMAN") {}

startup
{

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

}

update
{
    vars.reader.BaseStream.Position = 0;

    current.loadvalue = vars.reader.ReadUInt32();
	current.connectionstatus = vars.reader.ReadUInt32();
	current.slottype = vars.reader.ReadUInt32();
	current.slotnumber = vars.reader.ReadUInt32();
	current.idoflevelswitch = vars.reader.ReadUInt32();

}

start
{

}

reset
{

}

split
{
	if ((old.slottype == 0 || old.slottype == 2) && old.idoflevelswitch == 0 && current.idoflevelswitch == 10169)
	{
		return true;
	}
}

isLoading 
{
    if (current.slottype == 0 && (current.loadvalue > 0))
	{
		return false;
	}
	else if (current.loadvalue > 0)
	{
		return true;
	}
	else if (current.connectionstatus == 9)
	{
		return true;
	}
	else
	{
		return false;
	}
}