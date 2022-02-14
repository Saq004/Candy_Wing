using System;
using System.Collections.Generic;

[Serializable]
public class ItemList
{
    public List<Root> items = new List<Root>();
}

[Serializable]
public class Status
{
    public bool? verified { get; set; }
    public double sentCount { get; set; }
}


[Serializable]
public class Root
{
    public Status status { get; set; }

    public string _id { get; set; }
    public string user { get; set; }
    public string text { get; set; }
    public double __v { get; set; }
    public string source { get; set; }
    public DateTime updatedAt { get; set; }
    public string type { get; set; }
    public DateTime createdAt { get; set; }
    public bool deleted { get; set; }
    public bool used { get; set; }
}