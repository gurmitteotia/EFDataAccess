namespace GenRepo.Tests
{
    public class TestItem
    {
        private readonly int _id;

        public string Name;
        public int Salary;

        public TestItem(int id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is TestItem item &&
                   _id == item._id;
        }

        public override int GetHashCode()
        {
            return 2323 + _id.GetHashCode();
        }
    }
}