using System.Text;

namespace DLL
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


        public Node<T> forward(Node<T>  n, int count) {
                
                var node=n;
                
                for(int i=0;i<count;i++) {                
                if(node==tail || node.next==null)
                    node=head;
                 else node = node.next;   
                }
                return node;

        }

        public Node<T> back(Node<T>  n, int count) {
                var node = n;
                for(int i=0;i<count;i++) {                
                if(node==head || node.prev==null)
                    node=tail;
                 else node = node.prev;   
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
            } while(tmp != tail);
             yield return tail;
        }

        public void from_list(List<T> list)
        {
            foreach (var t in list)
            {
                insert_at_tail (t);
            }
        }

        public void from_list_reversed(List<T> list)
        {
            foreach (var t in list)
            {
                insert_at_head (t);
            }
        }

        public void insert_at_head(T value)
        {
            insert_at_head(new Node<T>(value));
        }

        public void insert_at_head(Node<T> n)
        {
            n.next = head;

            if (head != null)
            {
                head.prev = n;
            }
            head = n;
            if (head.next == null) tail = head;
            size++;
         
        }

        public void insert_at_tail(T value)
        {
            insert_at_tail(new Node<T>(value));
        }

        public void insert_at_tail(Node<T> n)
        {
            if (head == null)
            {
                insert_at_head (n);
                return;
            }
            tail.next=n;
            n.prev=tail;
            tail=n;
            size++;

        
        }


        public void Remove(Node<T> node)
        {
            if (node.prev != null) node.prev.next = node.next;
            if (node.next != null)
            {
                node.next.prev = node.prev;
                tail = node.prev;
            }
            if (size > 0) size--;
        }

        public void MoveToBefore(Node<T> before, Node<T> node)
        {
            InsertBetween(before.prev,before,node);
       }

        public void MoveAfter(Node<T> after, Node<T> node)
        {
            InsertBetween(after,after.next,node);
        }


        private void InsertBetween(Node<T> a, Node<T> b, Node<T> node) 
        {
           
            
            // fiurst take it out
            bool isTail = node==tail;
            bool isHead = node==head;


           // update refs
           if(node.prev!=null) {
                node.prev.next = node.next;
                if(isTail)
                    tail = node.prev;  
           }
           
           if(node.next!=null)  {
                node.next.prev=node.prev;     
                if(isHead) 
                    head = node.next;
                }

            // then insert it again

            node.prev=a;
            node.next=b;

            if(a!=null)
                a.next=node;
            else 
                head=node;    
            
            if(b!=null)    
                b.prev=node;
            else 
                tail=node;    
            
           
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
            while (temp != tail);
            if(tail!=null)
                b.Append($"[T] {temp.data.ToString()}  -->  {(temp.next==head ? "[H]" : "null")} ");

            return b.ToString();
        }
    }
}

