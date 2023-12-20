using System.Collections.Generic;

public class PrintItem
{
    public List<string> parameters = new List<string>();
    public PrintItem next;

    public PrintItem(List<string> param)
    {
        this.parameters = param;

    }
}