using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DMR
{
    public bool isSuccess { get; set; }
    public List<string> messages { get; set; }

    public DMR()
    {
        isSuccess = true;
        messages = new List<string>();
    }
}