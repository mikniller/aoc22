using System.Text;

namespace Aoc.Common 
{
    public class Node<T>
    {
        public Node<T> prev;

        public T data;

        public Node<T> next;

        public Node(T value)
        {
            prev = null;
            next = null;
            data = value;
        }
    }

    public class DoubleLinkedList<T>
    {

        public Node<T> head = null;
        public Node<T> tail = null;

        public int size;

        public DoubleLinkedList() {
        }

        public DoubleLinkedList(IEnumerable<T> list) {
            foreach(var n in list) {
                InsertEnd(new Node<T>(n));
            }    

        }


        public void InsertFront(Node<T> node) {
            if(head!=null)
                head.prev=node;
            node.next=head;
            head=node;    

            if(tail==null)
                tail = node;

            size++;    
        }

        public void InsertEnd(Node<T> node) {
            if(tail==null)
                InsertFront(node);
            else {
                tail.next = node;
                node.next = null;
                node.prev=tail;
                tail=node;
            }
            size++;
        }

        public Node<T> Remove(Node<T> n) {
           if(n.prev!=null && n.next!=null) {

                n.prev.next = n.next;
                n.next.prev = n.prev;
           }
           if(n==head) {
                head = n.next;
                n.next.prev = null;
           } 
           if(n==tail) {
                n.prev.next = null;
                tail = n.prev;
           }

           n.next = null;
           n.prev = null;
           size--;  
           return n;
        }     

        public Node<T> AddAfter(Node<T> afterNode, Node<T> toBeInserted) {
             if(afterNode==tail) {
                afterNode.next = toBeInserted;
                toBeInserted.prev = afterNode;
                tail = toBeInserted;
             }   else {
                 afterNode.next.prev=toBeInserted;
                 toBeInserted.next = afterNode.next;
                 afterNode.next = toBeInserted;
                 toBeInserted.prev = afterNode;   
             }
             size++;
             return toBeInserted;
        }

        
        public Node<T> Forward(Node<T>  n, int count) {
                
            var node=n;
            for(int i=0;i<count;i++) {                
                if(node.next==null)
                    node=head;
                 else node = node.next;   
                }
                return node;
        }

        public IEnumerable<Node<T>> AsList()
        {
            var tmp = head;
            do
            {
                yield return tmp;
                tmp = tmp.next;
            } while(tmp != null);
        }



      


        public string Print(Node<T> highlight)
        {
            StringBuilder b = new StringBuilder();
            Node<T> temp = head;
            do
            {
                b.Append($"{(temp == head ? "[H]" : "")} {temp.data.ToString()} {(highlight==temp ? "*" : "")}  --> ");
                temp = temp.next;
            }
            while (temp != null);

            return b.ToString();
        }
    }
}

