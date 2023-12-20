public class Queue
{
    object sync = new object();
    PrintItem head;
    PrintItem tail;

    public void Add(PrintItem pi)
    {
        lock (this.sync)
        {
            if (this.head == null)
            {
                this.head = pi;
                this.tail = pi;
            }
            else
            {
                this.tail.next = pi;
                this.tail = pi;
            }
        }
    }

    public PrintItem Pop()
    {
        lock (this.sync)
        {
            var ret = this.head;
            this.head = this.head.next;
            return ret;
        }
    }

    public bool Any()
    {
        lock (this.sync)
        {
            return this.head != null;
        }
    }
}