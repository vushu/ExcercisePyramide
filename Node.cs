using System.Collections.Generic;
using System.Linq;

namespace MidleTierExercise
{
    public class Node
    {
        public int Value { get; set; }
        public List<Node> Children = new List<Node>();
        public bool IsEven => Value % 2 == 0;

        public Node(int value)
        {
            Value = value;
        }

        public Node NextMaxChild()
        {
            if (IsEven)
            {
                //finding the max child which is Odd since parent is event
                return Children.Where(i => !i.IsEven).OrderByDescending(i => i.Value).FirstOrDefault();
            }
            // Odd
            else
            {
                //finding the max child which is even since parent is Odd
                return Children.Where(i => i.IsEven).OrderByDescending(i => i.Value).FirstOrDefault();
            }
        }
    }
}
