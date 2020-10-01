using System;
using System.Collections.Generic;
using System.Threading;

namespace TjuvOchPolis
{
    class City
    {
        public int citysizeX;
        public int citysizeY;





        public void PopulateCity(int width, int height, int civs, int cops, int pickpockets)
        {
            citysizeX = width;
            citysizeY = height;




            List<int> robberies = new List<int>();
            List<int> arrests = new List<int>();
            List<Person> jail = new List<Person>();
            List<Person> people = new List<Person>();


            Console.CursorVisible = false;

            Random random = new Random();

            for (int i = 0; i < pickpockets; i++)
            {
                Pickpocket pickpocket = new Pickpocket(random.Next(citysizeX), random.Next(citysizeY), 0);
                people.Add(pickpocket);

            }

            for (int i = 0; i < civs; i++)
            {
                Civ civ = new Civ(random.Next(citysizeX), random.Next(citysizeY));
                people.Add(civ);

            }

            for (int i = 0; i < cops; i++)
            {
                Cop cop = new Cop(random.Next(citysizeX), random.Next(citysizeY));
                people.Add(cop);

            }


            DrawAndMovement(people, jail, arrests, robberies);
        }


        public void DrawAndMovement(List<Person> people, List<Person> jail, List<int> arrests, List<int> robberies)
        {
            Random random2 = new Random();
            List<Person> outOfJail = new List<Person>();
            while (true)
            {

               


                foreach (Person person in people)
                {
                    person.MovePlayer(random2.Next(-1, 2), random2.Next(-1, 2));

                    if (person.Xposition < 0) { person.Xposition = citysizeX; }
                    if (person.Xposition > citysizeX) { person.Xposition = 0; }
                    if (person.Yposition < 0) { person.Yposition = citysizeY; }
                    if (person.Yposition > citysizeY) { person.Yposition = 0; }


                }
                foreach (var person in people)
                {
                    Console.SetCursorPosition(person.Xposition, person.Yposition);


                    if (person.Symbol == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(person.Symbol);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    else if (person.Symbol == 'T')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(person.Symbol);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (person.Symbol == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(person.Symbol);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                }
                LookforCollision(people, jail, arrests, robberies);

                foreach (var person in jail)
                {
                    if (person.GetJailTimer() < 100)
                    {
                        person.IncreaseJailTimer();
                        Console.SetCursorPosition(60, 27);
                        Console.WriteLine($"A pickpocket has been in jail for {person.GetJailTimer()}");


                    }
                    else
                    {
                        people.Add(person);
                        outOfJail.Add(person);
                        person.ResetJailTimer();
                        Console.SetCursorPosition(60, 28);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("A pickpocker has been released from jail");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(2000);
                    }


                }
                foreach (var person in outOfJail)
                {
                    jail.Remove(person);
                }
                Console.SetCursorPosition(0, 27);
                Thread.Sleep(50);
                Console.Clear();

            }

        }

        private static void LookforCollision(List<Person> people, List<Person> jail, List<int> arrests, List<int> robberies)
        {

            List<Person> jail2 = new List<Person>();
            Console.SetCursorPosition(0, 27);

            Console.Write($"Pickpockets in jail: {jail.Count}\n");
            Console.Write($"Citizens robbed: {robberies.Count}\n");
            Console.Write($"Pickpockets arrested: {arrests.Count}");

            foreach (var person in people)
            {
                foreach (var person2 in people)
                {
                    if (!person.Equals(person2))
                    {
                        if (person.Xposition == person2.Xposition && person.Yposition == person2.Yposition)
                        {
                            if (person is Pickpocket && person2 is Civ)
                            {
                                if (person2.DoIHaveItems() > 0)
                                {
                                    Backpack stolenGoods = person2.GetRandom();
                                    person.TakeItem(stolenGoods);
                                    person2.RemoveItem(stolenGoods);
                                    Console.SetCursorPosition(60, 29);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write($"A pickpocket stole {stolenGoods.Name} from citizen");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    robberies.Add(1);

                                    Console.SetCursorPosition(person.Xposition, person.Yposition);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write("X");
                                    Thread.Sleep(2000);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.SetCursorPosition(0, 27);

                                }
                            }
                            else if (person is Cop && person2 is Pickpocket)
                            {
                                if (person2.DoIHaveItems() > 0)
                                {
                                    List<Backpack> thiefBackpack = person2.TakeAllItems();
                                    foreach (var item in thiefBackpack)
                                    {
                                        person.TakeItem(item);
                                        person2.RemoveItem(item);
                                        Console.SetCursorPosition(60, 30);
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        Console.Write($"A cop took {item.Name} from the pickpocket");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(1000);

                                    }
                                    arrests.Add(1);
                                    jail2.Add(person2);
                                    Console.SetCursorPosition(person.Xposition, person.Yposition);
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.Write("X");
                                    Thread.Sleep(2000);
                                    Console.ForegroundColor = ConsoleColor.Gray;



                                }
                            }
                        }
                    }
                }

            }

            foreach (var pickpocket in jail2)
            {
                jail.Add(pickpocket);
                people.Remove(pickpocket);
            }



        }



    }


}
