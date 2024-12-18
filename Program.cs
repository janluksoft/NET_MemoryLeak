
namespace pN8_Leak
{

    //Example of a program that 'leaks' memory

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            while (true)
            {
                WorkClass cWork = new WorkClass();
                cWork.printEvent += (sender, e) => Console.WriteLine("Event triggered!");
                // The lambda expression [(sender, e) => Console.WriteLine("Event triggered!");] captures a external reference to 'cWork' and holds it
                // The completion of a single while iteration should delete the cWork object.
                // However, the lambda expression 'holds' a reference to 'cWork' and
                // the GC does not remove cWork from the heap.

                // Each 'while' iteration creates a new cWork object which is not deleted
                // and thus a memory leak occurs.
                // This memory leak is caused by the lambda expression not releasing the reference.

                cWork.RunEvent();
            }
        }
    }

    class WorkClass
    {
        public event EventHandler printEvent; // Event

        // event trigger
        public void RunEvent()
        {
            if (printEvent != null)
                printEvent.Invoke(this, EventArgs.Empty); //
            //Invoke method is Implicit for Delegates
            //Invoke calls all stored methods inside printEvent at once.
        }
    }

}
