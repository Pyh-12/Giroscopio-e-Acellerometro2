using System;
using System.Collections.Generic;

[Serializable]
public class StoreItemDto
{
    public string id;
    public string name;
    public int cost;
    public string imagepatch;   // caminho em Resources
    public string description;
    public bool purchased;
    public string prefabname;
    public string prefabpatch;  // caminho em Resources (opcional)
}

[Serializable]
public class StoreItemsWrapper
{
    public List<StoreItemDto> items;
}
