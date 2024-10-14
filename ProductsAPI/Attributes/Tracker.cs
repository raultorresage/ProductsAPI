namespace ProductsAPI.Attributes
{
    public class Tracker: Attribute
    {
        public string Route { get; }
        public Tracker(string route) {
            this.Route = route;
        }


    }
}
