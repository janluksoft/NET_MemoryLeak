# Memory leaks in .NET under C#

## Overview

Despite the existence of a separate Garbare Collection (GC) thread that 
manages memory, uncontrolled "memory leaks" can occur.
This program shows such a case.

The main cause here is not removing references to an instance that 
should cease to exist, but incorrectly holding references to it 
does not allow GC to remove such an instance.
As a result of creating new instances - they grow in memory 
like an avalanche and can lead to serious failures.

## Detailed description of the program


The lambda expression [(sender, e) => Console.WriteLine("Event triggered!");] captures a external reference to 'cWork' and holds it. The completion of a single while iteration should delete the cWork object.
However, the lambda expression 'holds' a reference to 'cWork' and the GC does not remove cWork from the heap.
Each 'while' iteration creates a new cWork object which is not deleted and thus a memory leak occurs. This memory leak is caused by the lambda expression not releasing the reference.

## Preventing memory leaks

Explicitly unsubscribe from the event when done:

```csharp
cWork.printEvent -= handler;
```

To diagnose and identify memory leaks, you can perform a memory dump 
and use the WinDbg debugger to analyze the state of the memory.