namespace Concert_CRUD_app;

public class Concert
{
        public int Id { get; set; }
        public string Location { get; set; }
        public string Performer { get; set; }
        public DateTime DateAndTime { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Performer} at {Location} on {DateAndTime:yyyy-MM-dd HH:mm}. Capacity: {Capacity}. Price: {Price:C} ";
        }
}