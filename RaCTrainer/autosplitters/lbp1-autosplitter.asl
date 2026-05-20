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

}

update
{
    vars.reader.BaseStream.Position = 0;

    current.loadvalue = vars.reader.ReadUInt32();
	current.connectionstatus = vars.reader.ReadUInt32();

}

start
{

}

reset
{

}

split
{
  
}

isLoading 
{
    
    return (current.loadvalue > 0) || (current.connectionstatus = 9);
}