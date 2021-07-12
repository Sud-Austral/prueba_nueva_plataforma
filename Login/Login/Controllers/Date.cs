namespace Login.Controllers
{
    internal class Date
    {
        private string v;

        public Date()
        {
        }

        public Date(string v)
        {
            this.v = v;
        }

        public int Days { get; internal set; }
        public double TotalSeconds { get; internal set; }
    }
}