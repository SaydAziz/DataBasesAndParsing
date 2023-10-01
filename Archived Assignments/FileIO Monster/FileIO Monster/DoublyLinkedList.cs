using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO_Monster
{
    internal class DoublyLinkedList<T>
    {
        public Node<T> Head {  get; set; }
        public Node<T> Tail { get; set; }
            
        public DoublyLinkedList(Node<T> head, Node<T> tail)
        {
            Head = head;
            Tail = tail;
        }

        public void Add(Node<T> node)
        {
            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Node<T> cachedTail = Tail;
                Tail.Next = node;
                Tail = node;
                Tail.Prev = cachedTail;
            }
        }

        public void PrintList()
        {
            
            Console.WriteLine(this.ToString());

        }

        public override string ToString()
        {
            string record = "";
            Node<T> currentNode = Head;
            while (currentNode.Next != null)
            {
                record = record + currentNode.Data + " ";
                currentNode = currentNode.Next;
            }
            record = record + currentNode.Data;
            return record;
        }

    }
}
