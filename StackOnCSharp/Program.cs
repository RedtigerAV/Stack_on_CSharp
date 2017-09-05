using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnCSharp {

    public class Node<T> {
        public T Data { get; set; }
        public Node<T> prev { get; set; }
        public Node<T> next { get; set; }
        
        public Node(T data) {
            Data = data;
            prev = null;
            next = null;
        }

        public Node() {
            Data = default(T);
            prev = null;
            next = null;
        }
    }

    public class MyStack<T>: IEnumerable<T>, IEnumerator<T> {
        private Node<T> Top { get; set; }

        private readonly Node<T> Root;

        public T Peek {
            get {
                if (Top == null)
                    throw new InvalidOperationException("Invalid attempt to invoke Peek property. Stack is empty");
                return Top.Data;
            }
        }

        Node<T> _selected;

        public T Current {
            get {
                return _selected.Data;
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public MyStack() { Top = null; _selected = null; Root = new Node<T>(); }
        public void push(T data) {
            Node<T> elem = new Node<T>(data);
            if (Top != null) {
                Top.next = elem;
                elem.prev = Top;
                Top = elem;
            }
            else {
                Top = elem;
                Root.next = Top;
                _selected = Root;
            }
        }

        public T pop() {
            if (Top == null)
                throw new InvalidOperationException("Invalid attempt to invoke pop() method. Stack is empty");
            T res = Top.Data;
            Top = Top.prev;
            if (Top != null)
                Top.next = null;
            else {
                Root.next = null;
                _selected = Root;
            }
            return res;
        }

        public IEnumerator<T> GetEnumerator() {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Dispose() { }

        public bool MoveNext() {
           
            if (_selected.next == null) {
                Reset();
                return false;
            }

            _selected = _selected.next;
            return true;
        }

        public void Reset() {
            _selected = Root;
        }

    }
    class Program {
        static void Main(string[] args) {
            try {
                MyStack<int> stack = new MyStack<int>();
                Console.WriteLine("All requests: push i/ pop / peek / all");
                do {
                    string[] quest;

                    quest = Console.ReadLine().Split();

                    if (quest[0] == "push") {
                        stack.push(int.Parse(quest[1]));
                    } else if (quest[0] == "pop") {
                        stack.pop();
                    } else if (quest[0] == "peek") {
                        Console.WriteLine(stack.Peek);
                    } else if (quest[0] == "all") {
                        foreach (var el in stack)
                            Console.Write(el + " ");
                        Console.WriteLine();
                    } else {
                        Console.WriteLine("Please, repeat your request");
                    }

                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("Something wrong: \n" + ex.Message);
            }
        }
    }
}
