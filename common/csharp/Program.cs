// See https://aka.ms/new-console-template for more information
using Aoc.Common;

Console.WriteLine("-- 2021 -----------------------------------------------------------------------------------------------\n");


//Util.RunAllSolved(2021, 1, new ConsoleWriter(),true,true);

Util.Run(4,2021,true,1,new ConsoleWriter());
Util.Run(4,2021, false, 1, new ConsoleWriter());



Console.WriteLine("-------------------------------------------------------------------------------------------------------\n");
Console.WriteLine("Hit enter to close");

Console.ReadLine();