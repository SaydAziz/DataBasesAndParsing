using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FileIO_Monster
{
    internal class Game
    {
        DoublyLinkedList<string> Record;
        List<Entity> Enemies;
        Entity Player;
        bool playing;
        int kills;
        public Game(List<Entity> enemies)
        {
            Enemies = enemies;
            playing = true;
            kills = 0;

            Record = new DoublyLinkedList<string>(null, null);
        }

        public string StartGame()
        {
            Console.WriteLine("WELCOME TO THE DUNGEON RAHHHHHHHHHHHHH");

            Player = MakeHero();


            foreach (var enemy in Enemies)
            {
                Console.WriteLine(enemy.ToString());
            }


            while (playing)
            {
                LobbyRoom();
            }

            Record.PrintList();
            return Record.ToString();
        }

        void LobbyRoom()
         {
            if (kills < Enemies.Count())
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Console.WriteLine($"Room {i + 1}: {Enemies[i].Type}");
                }


                while (playing)
                {
                    Console.WriteLine("Choose a room to clear: ");
                    Entity room = Enemies[Int32.Parse(Console.ReadLine()) - 1];

                    if (!room.Dead)
                    {
                        if (Fight(Player, room))
                        {
                            room.Kill();
                            Console.WriteLine("YOU WIN!!");
                            kills++;

                            Record.Add(new Node<string>("WIN"));
                            return;
                        }
                        else
                        {
                            Console.WriteLine("YOU LOSE!!");
                            playing = false;
                            Record.Add(new Node<string>("LOSE"));

                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That room is already cleared. Please select another.");
                    }
                }
            }
            else
            {
                playing = false;
            }
           

        }

        Entity MakeHero()
        {
            Console.WriteLine("CREATE YOUR HERO");

            Console.WriteLine("Enter your hero's HP: ");
            int hp = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter your hero's MP: ");
            int mp = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter your hero's AP: ");
            int ap = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter your hero's DEF: ");
            int def = Int32.Parse(Console.ReadLine());

            return new Entity("Player", hp, mp, ap, def);
        }

        bool Fight(Entity player, Entity enemy)
        {
            int pPoints = 0;
            int ePoints = 0;


            if (player.HP > enemy.HP)
            {
                pPoints++;
            }
            else
            {
                ePoints++;
            }

            if (player.MP > enemy.MP)
            {
                pPoints++;
            }
            else
            {
                ePoints++;
            }

            if (player.AP > enemy.AP)
            {
                pPoints++;
            }
            else
            {
                ePoints++;
            }

            if (player.DEF > enemy.DEF)
            {
                pPoints++;
            }
            else
            {
                ePoints++;
            }

            return (pPoints > ePoints) ? true : false;
        }
    }
}
